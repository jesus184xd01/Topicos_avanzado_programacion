using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace HILO
{
    public partial class Form1 : Form
    {
        // ── Hilo y sincronización 
        private Thread _hilo;
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
        private CancellationTokenSource _cts;
        private volatile bool _detenido = false;

        // ── Imágenes 
        private List<string> _imagePaths = new List<string>();
        private PictureBox[] _pictureBoxes;

        // ── Animación: un Timer y contadores por PictureBox 
        private System.Windows.Forms.Timer _animTimer;
        private int[] _animStep = new int[6];   // frame actual (0-19)
        private bool[] _animActive = new bool[6];  // si está parpadeando
        private Panel[] _pbPanels = new Panel[6]; // panel contenedor con borde de color

        // Colores de borde por slot (uno distinto para cada PictureBox)
        private static readonly Color[] SlotColors = new Color[]
        {
            Color.FromArgb(255,  80, 120),  // rosa
            Color.FromArgb( 80, 160, 255),  // azul
            Color.FromArgb( 60, 210, 130),  // verde menta
            Color.FromArgb(255, 170,  50),  // naranja
            Color.FromArgb(170,  80, 255),  // violeta
            Color.FromArgb( 50, 210, 210),  // cian
        };

        // ── Paleta de colores de estado ───────────────────────────────────────
        private static readonly Color ColorNormal = Color.LightGray;
        private static readonly Color ColorNoIniciado = Color.Gold;
        private static readonly Color ColorEjecutando = Color.LightGreen;
        private static readonly Color ColorSuspendido = Color.SkyBlue;
        private static readonly Color ColorDetenido = Color.LightCoral;

        //  Constructor
        public Form1()
        {
            InitializeComponent();

            _pictureBoxes = new PictureBox[]
            {
                pictureBox1, pictureBox2, pictureBox3,
                pictureBox4, pictureBox5, pictureBox6
            };

            ConfigurarPictureBoxes();
            ConfigurarAnimTimer();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 6;
            progressBar1.Value = 0;

            EstablecerEstado("No iniciado");
            ActualizarBotones(canStart: false, running: false, suspended: false);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Estilo visual de los PictureBox
        // ═════════════════════════════════════════════════════════════════════
        private void ConfigurarPictureBoxes()
        {
            for (int i = 0; i < _pictureBoxes.Length; i++)
            {
                var pb = _pictureBoxes[i];

                // Fondo degradado oscuro mientras espera imagen
                pb.BackColor = Color.FromArgb(30, 30, 45);
                pb.SizeMode = PictureBoxSizeMode.Zoom;

                // Borde redondeado: usamos un Panel padre pintado a mano
                // (solo si el PictureBox tiene un Parent que podamos decorar)
                pb.Paint += PictureBox_Paint;
            }
        }

        // Dibuja un marco de color + esquinas redondeadas sobre cada PictureBox
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            var pb = (PictureBox)sender;
            int idx = Array.IndexOf(_pictureBoxes, pb);
            if (idx < 0) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // ── Fondo degradado si no tiene imagen ────────────────────────────
            if (pb.Image == null)
            {
                Color c1 = Color.FromArgb(28, 28, 48);
                Color c2 = Color.FromArgb(45, 45, 75);
                using (var br = new LinearGradientBrush(
                    pb.ClientRectangle, c1, c2, LinearGradientMode.ForwardDiagonal))
                {
                    g.FillRectangle(br, pb.ClientRectangle);
                }

                // Texto placeholder
                using (var f = new Font("Segoe UI", 9, FontStyle.Italic))
                using (var sb = new SolidBrush(Color.FromArgb(100, 255, 255, 255)))
                {
                    var fmt = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString($"Imagen {idx + 1}", f, sb, pb.ClientRectangle, fmt);
                }
            }

            // ── Marco de color del slot ────────────────────────────────────────
            Color borderColor = SlotColors[idx];
            int thick = _animActive[idx] ? 4 : 2;

            // Brillo pulsante: interpolamos alpha durante la animación
            if (_animActive[idx])
            {
                double t = (double)_animStep[idx] / 20.0;
                double glow = Math.Sin(t * Math.PI);          // 0→1→0
                int a = (int)(80 + 175 * glow);
                borderColor = Color.FromArgb(a, borderColor);
            }

            using (var pen = new Pen(borderColor, thick))
            {
                pen.LineJoin = LineJoin.Round;
                var r = new Rectangle(1, 1, pb.Width - 3, pb.Height - 3);
                g.DrawRectangle(pen, r);
            }

            // ── Número de slot (esquina superior izquierda) ───────────────────
            using (var f = new Font("Segoe UI", 8, FontStyle.Bold))
            using (var bg = new SolidBrush(Color.FromArgb(160, SlotColors[idx])))
            using (var fg = new SolidBrush(Color.White))
            {
                var badge = new RectangleF(4, 4, 22, 18);
                g.FillRectangle(bg, badge);
                var fmt = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString((idx + 1).ToString(), f, fg, badge, fmt);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Timer de animación (60 ms ≈ 16 fps)
        // ═════════════════════════════════════════════════════════════════════
        private void ConfigurarAnimTimer()
        {
            _animTimer = new System.Windows.Forms.Timer();
            _animTimer.Interval = 60;
            _animTimer.Tick += AnimTimer_Tick;
            _animTimer.Start();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            bool needRefresh = false;
            for (int i = 0; i < 6; i++)
            {
                if (!_animActive[i]) continue;
                _animStep[i]++;
                if (_animStep[i] >= 20)
                {
                    _animStep[i] = 0;
                    _animActive[i] = false;   // animación terminó
                }
                _pictureBoxes[i].Invalidate();
                needRefresh = true;
            }
        }

        /// <summary>Dispara la animación de "aparición" en el PictureBox idx.</summary>
        private void AnimarSlot(int idx)
        {
            _animStep[idx] = 0;
            _animActive[idx] = true;
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Eventos de labels y PictureBoxes (requeridos por el diseñador)
        // ═════════════════════════════════════════════════════════════════════
        private void lbl_estado_actual_Click(object sender, EventArgs e) { }
        private void lbl_no_iniciado_Click(object sender, EventArgs e) { }
        private void lbl_ejecutando_Click(object sender, EventArgs e) { }
        private void lbl_suspendido_Click(object sender, EventArgs e) { }
        private void lbl_detenido_Click(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void pictureBox4_Click(object sender, EventArgs e) { }
        private void pictureBox5_Click(object sender, EventArgs e) { }
        private void pictureBox6_Click(object sender, EventArgs e) { }

        // ═════════════════════════════════════════════════════════════════════
        //  BOTÓN: Seleccionar carpeta
        // ═════════════════════════════════════════════════════════════════════
        private void btn_selectfolder_Click(object sender, EventArgs e)
        {
            DetenerHiloInterno();

            using (var dlg = new FolderBrowserDialog
            {
                Description = "Selecciona la carpeta con imágenes"
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                var exts = new[] { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif", "*.webp" };
                _imagePaths.Clear();

                foreach (var ext in exts)
                {
                    _imagePaths.AddRange(
                        Directory.GetFiles(dlg.SelectedPath, ext, SearchOption.TopDirectoryOnly));
                    if (_imagePaths.Count >= 6) break;
                }

                if (_imagePaths.Count > 6)
                    _imagePaths = _imagePaths.GetRange(0, 6);

                if (_imagePaths.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron imágenes en la carpeta seleccionada.",
                        "Sin imágenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            LimpiarGaleria();
            progressBar1.Maximum = _imagePaths.Count;
            progressBar1.Value = 0;
            EstablecerEstado("No iniciado");
            ActualizarBotones(canStart: true, running: false, suspended: false);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  BOTÓN: Iniciar hilo
        // ═════════════════════════════════════════════════════════════════════
        private void btn_start_Click(object sender, EventArgs e)
        {
            if (_imagePaths.Count == 0)
            {
                MessageBox.Show(
                    "Primero selecciona una carpeta con imágenes.",
                    "Sin imágenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LimpiarGaleria();
            progressBar1.Value = 0;

            _detenido = false;
            _pauseEvent = new ManualResetEventSlim(true);
            _cts = new CancellationTokenSource();

            _hilo = new Thread(() => EjecutarHilo(_cts.Token))
            {
                IsBackground = true,
                Name = "HiloGaleria"
            };

            EstablecerEstado("Ejecutando");
            ActualizarBotones(canStart: false, running: true, suspended: false);
            _hilo.Start();
        }

        // ═════════════════════════════════════════════════════════════════════
        //  BOTÓN: Suspender
        // ═════════════════════════════════════════════════════════════════════
        private void btn_suspender_Click(object sender, EventArgs e)
        {
            if (_hilo == null || !_hilo.IsAlive) return;
            _pauseEvent.Reset();
            EstablecerEstado("Suspendido");
            ActualizarBotones(canStart: false, running: false, suspended: true);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  BOTÓN: Reanudar
        // ═════════════════════════════════════════════════════════════════════
        private void btn_reanudar_Click(object sender, EventArgs e)
        {
            if (_hilo == null || !_hilo.IsAlive) return;
            _pauseEvent.Set();
            EstablecerEstado("Ejecutando");
            ActualizarBotones(canStart: false, running: true, suspended: false);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  BOTÓN: Detener hilo
        // ═════════════════════════════════════════════════════════════════════
        private void btn_detener_Click(object sender, EventArgs e)
        {
            DetenerHiloInterno();
            EstablecerEstado("Detenido");
            ActualizarBotones(canStart: _imagePaths.Count > 0, running: false, suspended: false);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Lógica del hilo secundario
        // ═════════════════════════════════════════════════════════════════════
        private void EjecutarHilo(CancellationToken token)
        {
            for (int i = 0; i < _imagePaths.Count; i++)
            {
                if (token.IsCancellationRequested || _detenido) break;

                _pauseEvent.Wait(token);
                if (token.IsCancellationRequested || _detenido) break;

                string path = _imagePaths[i];
                int idx = i;

                try
                {
                    var img = Image.FromFile(path);

                    Invoke((Action)(() =>
                    {
                        _pictureBoxes[idx].Image = img;
                        progressBar1.Value = idx + 1;
                        AnimarSlot(idx);              // ← dispara el efecto de brillo
                        _pictureBoxes[idx].Invalidate();
                    }));
                }
                catch { /* imagen corrupta: omitir */ }

                token.WaitHandle.WaitOne(1000);
            }

            if (!token.IsCancellationRequested && !_detenido)
            {
                Invoke((Action)(() =>
                {
                    EstablecerEstado("Detenido");
                    ActualizarBotones(canStart: true, running: false, suspended: false);
                }));
            }
        }

        private void DetenerHiloInterno()
        {
            _detenido = true;
            _pauseEvent?.Set();
            _cts?.Cancel();
            if (_hilo != null && _hilo.IsAlive)
                _hilo.Join(1500);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Helpers de UI
        // ═════════════════════════════════════════════════════════════════════
        private void EstablecerEstado(string estado)
        {
            if (InvokeRequired) { Invoke((Action)(() => EstablecerEstado(estado))); return; }

            lbl_estado_actual.Text = "Estado actual del hilo:  " + estado;

            lbl_no_iniciado.BackColor = ColorNormal;
            lbl_ejecutando.BackColor = ColorNormal;
            lbl_suspendido.BackColor = ColorNormal;
            lbl_detenido.BackColor = ColorNormal;

            switch (estado)
            {
                case "No iniciado": lbl_no_iniciado.BackColor = ColorNoIniciado; break;
                case "Ejecutando": lbl_ejecutando.BackColor = ColorEjecutando; break;
                case "Suspendido": lbl_suspendido.BackColor = ColorSuspendido; break;
                case "Detenido": lbl_detenido.BackColor = ColorDetenido; break;
            }
        }

        private void ActualizarBotones(bool canStart, bool running, bool suspended)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => ActualizarBotones(canStart, running, suspended)));
                return;
            }
            btn_start.Enabled = canStart;
            btn_suspender.Enabled = running;
            btn_reanudar.Enabled = suspended;
            btn_detener.Enabled = running || suspended;
            btn_selectfolder.Enabled = true;
        }

        private void LimpiarGaleria()
        {
            if (InvokeRequired) { Invoke((Action)LimpiarGaleria); return; }
            for (int i = 0; i < _pictureBoxes.Length; i++)
            {
                _pictureBoxes[i].Image?.Dispose();
                _pictureBoxes[i].Image = null;
                _animActive[i] = false;
                _animStep[i] = 0;
                _pictureBoxes[i].Invalidate();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _animTimer?.Stop();
            DetenerHiloInterno();
            base.OnFormClosing(e);
        }
    }
}