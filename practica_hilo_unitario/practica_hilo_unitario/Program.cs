using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace practica_hilo_unitario
{
    internal static class Program
    {
        // inicio .start
        // bloqueado .join
        // dormido .sleep
        // terminado .stop
        public class FormGaleria : Form1
        {
            // ── Controles de UI ──────────────────────────────────────────────
            private Label lblEstado;
            private Label lblTitulo;
            private Label lblInfo;
            private Button btnSeleccionar;
            private Button btnIniciar;
            private Button btnSuspender;
            private Button btnReanudar;
            private Button btnDetener;
            private Panel panelGaleria;
            private PictureBox[] pictureBoxes = new PictureBox[6];
            private Panel panelEstados;
            private Label[] lblEstadosBadge = new Label[4];
            private ProgressBar progressBar;
            private Panel panelBarra;

            // ── Hilo y estado ────────────────────────────────────────────────
            private Thread hiloGaleria;
            private string[] rutasImagenes;
            private bool pausado = false;
            private bool detenido = false;
            private readonly object lockPausa = new object();

            // Nombres de los 4 estados del hilo empleado
            private readonly string[] ESTADOS = {
            "Unstarted\n(No iniciado)",
            "Running\n(Ejecutando)",
            "Suspended\n(Suspendido)",
            "Stopped\n(Detenido)"
        };
            private readonly Color[] COLORES_ESTADO = {
            Color.FromArgb(255, 193, 7),    // Amarillo - Unstarted
            Color.FromArgb(40, 167, 69),    // Verde    - Running
            Color.FromArgb(23, 162, 184),   // Cyan     - Suspended
            Color.FromArgb(220, 53, 69)     // Rojo     - Stopped
        };

            // ── Constructor ──────────────────────────────────────────────────
            public FormGaleria()
            {
                InitializeComponents();
                ActualizarEstadoLabel("UNSTARTED", 0);
            }

            // ── Construcción de interfaz ─────────────────────────────────────
            private void InitializeComponents()
            {
                this.Text = "Galería — Estados del Hilo (Thread States)";
                this.Size = new Size(900, 720);
                this.MinimumSize = new Size(900, 720);
                this.BackColor = Color.FromArgb(18, 18, 28);
                this.ForeColor = Color.White;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Font = new Font("Segoe UI", 9f);

                // ── Título ───────────────────────────────────────────────────
                lblTitulo = new Label
                {
                    Text = "🖼  GALERÍA DE IMÁGENES — ESTADOS DEL HILO",
                    Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(200, 180, 255),
                    AutoSize = false,
                    Size = new Size(880, 36),
                    Location = new Point(10, 10),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblTitulo);

                // ── Panel de estados (badges) ────────────────────────────────
                panelEstados = new Panel
                {
                    Size = new Size(860, 60),
                    Location = new Point(20, 52),
                    BackColor = Color.FromArgb(30, 30, 45)
                };
                panelEstados.Paint += (s, e) =>
                {
                    var pen = new Pen(Color.FromArgb(80, 80, 120), 1);
                    e.Graphics.DrawRectangle(pen, 0, 0, panelEstados.Width - 1, panelEstados.Height - 1);
                };

                string[] iconos = { "⏸", "▶", "⏯", "⏹" };
                for (int i = 0; i < 4; i++)
                {
                    int x = 10 + i * 213;
                    lblEstadosBadge[i] = new Label
                    {
                        Text = iconos[i] + "  " + ESTADOS[i],
                        Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.FromArgb(50, 50, 70),
                        AutoSize = false,
                        Size = new Size(200, 44),
                        Location = new Point(x, 8),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Tag = i
                    };
                    panelEstados.Controls.Add(lblEstadosBadge[i]);
                }
                this.Controls.Add(panelEstados);

                // ── Label estado actual ──────────────────────────────────────
                lblEstado = new Label
                {
                    Text = "Estado actual del hilo:  ● UNSTARTED",
                    Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                    ForeColor = COLORES_ESTADO[0],
                    AutoSize = false,
                    Size = new Size(860, 32),
                    Location = new Point(20, 120),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                this.Controls.Add(lblEstado);

                // ── ProgressBar ──────────────────────────────────────────────
                panelBarra = new Panel
                {
                    Size = new Size(860, 14),
                    Location = new Point(20, 158),
                    BackColor = Color.FromArgb(40, 40, 60)
                };
                progressBar = new ProgressBar
                {
                    Minimum = 0,
                    Maximum = 6,
                    Value = 0,
                    Size = new Size(860, 14),
                    Location = new Point(0, 0),
                    Style = ProgressBarStyle.Continuous
                };
                panelBarra.Controls.Add(progressBar);
                this.Controls.Add(panelBarra);

                // ── Panel galería ────────────────────────────────────────────
                panelGaleria = new Panel
                {
                    Size = new Size(860, 340),
                    Location = new Point(20, 182),
                    BackColor = Color.FromArgb(25, 25, 40)
                };
                panelGaleria.Paint += (s, e) =>
                {
                    var pen = new Pen(Color.FromArgb(80, 80, 130), 1);
                    e.Graphics.DrawRectangle(pen, 0, 0, panelGaleria.Width - 1, panelGaleria.Height - 1);
                };

                // 6 PictureBoxes en 2 filas × 3 columnas
                for (int i = 0; i < 6; i++)
                {
                    int col = i % 3;
                    int row = i / 3;
                    pictureBoxes[i] = new PictureBox
                    {
                        Size = new Size(270, 155),
                        Location = new Point(10 + col * 283, 10 + row * 168),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.FromArgb(40, 40, 60),
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
                    // Placeholder gris con número
                    pictureBoxes[i].Paint += (s, e) =>
                    {
                        if (((PictureBox)s).Image == null)
                        {
                            var pb = (PictureBox)s;
                            int idx = Array.IndexOf(pictureBoxes, pb);
                            e.Graphics.FillRectangle(
                                new SolidBrush(Color.FromArgb(35, 35, 55)), pb.ClientRectangle);
                            var sf = new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            e.Graphics.DrawString(
                                $"[ {idx + 1} ]",
                                new Font("Segoe UI", 16f, FontStyle.Bold),
                                new SolidBrush(Color.FromArgb(80, 80, 110)),
                                pb.ClientRectangle, sf);
                        }
                    };
                    panelGaleria.Controls.Add(pictureBoxes[i]);
                }
                this.Controls.Add(panelGaleria);

                // ── Info ruta ────────────────────────────────────────────────
                lblInfo = new Label
                {
                    Text = "Ninguna carpeta seleccionada.",
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                    ForeColor = Color.FromArgb(140, 140, 180),
                    AutoSize = false,
                    Size = new Size(860, 20),
                    Location = new Point(20, 530),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                this.Controls.Add(lblInfo);

                // ── Botones ──────────────────────────────────────────────────
                btnSeleccionar = CrearBoton("📁  Seleccionar Carpeta", 20, 560,
                    Color.FromArgb(60, 60, 100), this.BtnSeleccionar_Click);

                btnIniciar = CrearBoton("▶  Iniciar Hilo", 190, 560,
                    Color.FromArgb(40, 120, 60), this.BtnIniciar_Click);

                btnSuspender = CrearBoton("⏸  Suspender", 360, 560,
                    Color.FromArgb(30, 110, 160), this.BtnSuspender_Click);

                btnReanudar = CrearBoton("▶▶  Reanudar", 530, 560,
                    Color.FromArgb(80, 60, 160), this.BtnReanudar_Click);

                btnDetener = CrearBoton("⏹  Detener Hilo", 700, 560,
                    Color.FromArgb(160, 40, 40), this.BtnDetener_Click);

                btnSuspender.Enabled = false;
                btnReanudar.Enabled = false;
                btnDetener.Enabled = false;
                btnIniciar.Enabled = false;

                this.Controls.AddRange(new Control[]
                    { btnSeleccionar, btnIniciar, btnSuspender,
                  btnReanudar,    btnDetener });

                // Leyenda inferior
                var lblLeyenda = new Label
                {
                    Text = "ℹ  El hilo pasa por 4 estados: Unstarted → Running → Suspended → Stopped",
                    Font = new Font("Segoe UI", 8f),
                    ForeColor = Color.FromArgb(120, 120, 160),
                    AutoSize = false,
                    Size = new Size(860, 20),
                    Location = new Point(20, 610),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblLeyenda);
            }

            private Button CrearBoton(string texto, int x, int y, Color fondo, EventHandler handler)
            {
                var btn = new Button
                {
                    Text = texto,
                    Size = new Size(155, 38),
                    Location = new Point(x, y),
                    BackColor = fondo,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(fondo.R + 30, fondo.G + 30, fondo.B + 30);
                btn.FlatAppearance.BorderSize = 1;
                btn.Click += handler;
                return btn;
            }

            // ── Seleccionar carpeta ──────────────────────────────────────────
            private void BtnSeleccionar_Click(object sender, EventArgs e)
            {
                var dlg = new FolderBrowserDialog
                {
                    Description = "Selecciona la carpeta con imágenes"
                };
                if (dlg.ShowDialog() != DialogResult.OK) return;

                // Buscar hasta 6 imágenes en la carpeta
                var exts = new[] { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif", "*.webp" };
                var lista = new System.Collections.Generic.List<string>();

                foreach (var ext in exts)
                {
                    lista.AddRange(Directory.GetFiles(dlg.SelectedPath, ext,
                        SearchOption.TopDirectoryOnly));
                    if (lista.Count >= 6) break;
                }

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron imágenes en la carpeta seleccionada.",
                        "Sin imágenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rutasImagenes = lista.GetRange(0, Math.Min(6, lista.Count)).ToArray();
                lblInfo.Text = $"📂  {dlg.SelectedPath}  —  {rutasImagenes.Length} imagen(es) encontrada(s)";

                // Limpiar galería previa
                LimpiarGaleria();
                progressBar.Value = 0;
                btnIniciar.Enabled = true;
                ActualizarEstadoLabel("UNSTARTED", 0);
            }

            // ── Iniciar hilo ─────────────────────────────────────────────────
            private void BtnIniciar_Click(object sender, EventArgs e)
            {
                // Reiniciar flags
                pausado = false;
                detenido = false;
                LimpiarGaleria();
                progressBar.Value = 0;

                // Crear hilo empleado (worker thread en un solo hilo)
                hiloGaleria = new Thread(CargarImagenes)
                {
                    Name = "HiloGaleria",
                    IsBackground = true   // se libera al cerrar el form
                };

                btnIniciar.Enabled = false;
                btnSuspender.Enabled = true;
                btnDetener.Enabled = true;
                btnReanudar.Enabled = false;

                // ESTADO 1: Running — el hilo arranca
                ActualizarEstadoLabel("RUNNING", 1);
                hiloGaleria.Start();
            }

            // ── Suspender hilo ───────────────────────────────────────────────
            private void BtnSuspender_Click(object sender, EventArgs e)
            {
                if (hiloGaleria == null || !hiloGaleria.IsAlive) return;
                pausado = true;
                // ESTADO 3: Suspended
                ActualizarEstadoLabel("SUSPENDED", 2);
                btnSuspender.Enabled = false;
                btnReanudar.Enabled = true;
            }

            // ── Reanudar hilo ────────────────────────────────────────────────
            private void BtnReanudar_Click(object sender, EventArgs e)
            {
                if (hiloGaleria == null || !hiloGaleria.IsAlive) return;
                pausado = false;
                lock (lockPausa) Monitor.PulseAll(lockPausa);
                // ESTADO 2: Running de nuevo
                ActualizarEstadoLabel("RUNNING", 1);
                btnSuspender.Enabled = true;
                btnReanudar.Enabled = false;
            }

            // ── Detener hilo ─────────────────────────────────────────────────
            private void BtnDetener_Click(object sender, EventArgs e)
            {
                detenido = true;
                pausado = false;
                lock (lockPausa) Monitor.PulseAll(lockPausa); // desbloquea si estaba pausado
                                                              // ESTADO 4: Stopped
                ActualizarEstadoLabel("STOPPED", 3);
                btnSuspender.Enabled = false;
                btnReanudar.Enabled = false;
                btnDetener.Enabled = false;
                btnIniciar.Enabled = true;
            }

            // ── Lógica del hilo: carga de imágenes ───────────────────────────
            private void CargarImagenes()
            {
                for (int i = 0; i < rutasImagenes.Length; i++)
                {
                    // ¿Se pidió detener?
                    if (detenido) break;

                    // ¿Está suspendido? → esperar
                    lock (lockPausa)
                    {
                        while (pausado && !detenido)
                            Monitor.Wait(lockPausa);
                    }

                    if (detenido) break;

                    // Cargar imagen (puede lanzar excepción → mostrar placeholder)
                    Image img = null;
                    try { img = Image.FromFile(rutasImagenes[i]); }
                    catch { img = null; }

                    // Capturar índice e imagen para el closure
                    int idx = i;
                    Image imagenFin = img;

                    // Actualizar UI en el hilo principal
                    this.Invoke((Action)(() =>
                    {
                        if (imagenFin != null)
                            pictureBoxes[idx].Image = imagenFin;
                        else
                            pictureBoxes[idx].Invalidate(); // muestra placeholder
                        progressBar.Value = idx + 1;
                    }));

                    // Pequeña pausa para visualizar la carga progresiva
                    Thread.Sleep(800);
                }

                // Hilo terminó su trabajo → STOPPED
                if (!detenido)
                {
                    this.Invoke((Action)(() =>
                    {
                        ActualizarEstadoLabel("STOPPED", 3);
                        btnSuspender.Enabled = false;
                        btnReanudar.Enabled = false;
                        btnDetener.Enabled = false;
                        btnIniciar.Enabled = true;
                    }));
                }
            }

            // ── Actualizar label y badges ────────────────────────────────────
            private void ActualizarEstadoLabel(string estado, int indice)
            {
                // Este método se llama desde el hilo principal o mediante Invoke
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => ActualizarEstadoLabel(estado, indice)));
                    return;
                }

                string[] iconos = { "⏸", "▶", "⏯", "⏹" };
                lblEstado.Text = $"Estado actual del hilo:  {iconos[indice]} {estado}";
                lblEstado.ForeColor = COLORES_ESTADO[indice];

                // Resaltar badge activo, opacar los demás
                for (int i = 0; i < 4; i++)
                {
                    lblEstadosBadge[i].BackColor = (i == indice)
                        ? COLORES_ESTADO[i]
                        : Color.FromArgb(50, 50, 70);
                    lblEstadosBadge[i].ForeColor = (i == indice)
                        ? Color.Black
                        : Color.FromArgb(140, 140, 180);
                }
            }

            // ── Limpiar galería ──────────────────────────────────────────────
            private void LimpiarGaleria()
            {
                foreach (var pb in pictureBoxes)
                {
                    pb.Image?.Dispose();
                    pb.Image = null;
                    pb.Invalidate();
                }
            }

            // ── Cierre del form ──────────────────────────────────────────────
            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                detenido = true;
                pausado = false;
                lock (lockPausa) Monitor.PulseAll(lockPausa);
                hiloGaleria?.Join(500);
                base.OnFormClosing(e);
            }

            // ── Entry point ──────────────────────────────────────────────────
            [STAThread]
            public static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormGaleria());
            }
        }
    }
}
