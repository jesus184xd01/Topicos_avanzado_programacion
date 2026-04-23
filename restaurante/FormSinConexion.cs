using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace restaurante
{
    public partial class FormSinConexion : Form
    {
        public FormSinConexion()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            this.Text = "Sin conexión";
            this.Size = new Size(580, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(245, 240, 230);

            // ── Imagen ────────────────────────────────────────────
            var pic = new PictureBox
            {
                Location = new Point(155, 25),
                Size = new Size(160, 160),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            try
            {
                string ruta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "img/sin_conexion.png");
                if (File.Exists(ruta))
                {
                    byte[] bytes = File.ReadAllBytes(ruta);
                    using (var ms = new MemoryStream(bytes))
                        pic.Image = new Bitmap(ms);
                }
            }
            catch { }

            // ── Título ────────────────────────────────────────────
            var lblTitulo = new Label
            {
                Text = "Sin conexión a internet",
                Font = new Font("Segoe UI", 15f, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 50, 20),
                AutoSize = false,
                Size = new Size(440, 38),
                Location = new Point(20, 200),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ── Mensaje ───────────────────────────────────────────
            var lblMensaje = new Label
            {
                Text = "La aplicación está configurada en modo remoto\n" +
                            "pero no se detectó conexión a internet.\n\n" +
                            "Verifica tu conexión e intenta de nuevo.",
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(80, 60, 40),
                AutoSize = false,
                Size = new Size(420, 80),
                Location = new Point(30, 245),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ── Botón reintentar ──────────────────────────────────
            var btnReintentar = new Button
            {
                Text = "Reintentar",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Size = new Size(140, 38),
                Location = new Point(90, 338),
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnReintentar.FlatAppearance.BorderSize = 0;
            btnReintentar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            };

            // ── Botón salir ───────────────────────────────────────
            var btnSalir = new Button
            {
                Text = "Salir",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Size = new Size(140, 38),
                Location = new Point(248, 338),
                BackColor = Color.FromArgb(198, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[]
            {
                pic, lblTitulo, lblMensaje, btnReintentar, btnSalir
            });
        }

        private void FormSinConexion_Load(object sender, EventArgs e) { }
    }
}