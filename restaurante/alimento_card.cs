using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurante
{
    public partial class alimento_card : Form
    {
        private static readonly string RutaBase =
            AppDomain.CurrentDomain.BaseDirectory;

        private string _imagenUrl;
        private int _id;
        private string _tipo;
        private string _nombre;
        private decimal _precio;

        public alimento_card() { InitializeComponent(); }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        // ── Se dispara cuando el handle está listo ────────────────
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (_imagenUrl != null)
                CargarImagenAsync(_imagenUrl);
        }

        // ── Carga y redimensiona en segundo plano ─────────────────
        private async void CargarImagenAsync(string imagenUrl)
        {
            if (IsDisposed || !IsHandleCreated) return;

            img_icono.BackColor = Color.FromArgb(220, 220, 220);
            img_icono.Image = null;
            img_icono.SizeMode = PictureBoxSizeMode.Zoom;

            int maxAncho = img_icono.Width > 0 ? img_icono.Width : 200;
            int maxAlto = img_icono.Height > 0 ? img_icono.Height : 200;

            try
            {
                string ruta = Path.Combine(RutaBase, imagenUrl ?? "img/default.jpg");
                string rutaDefault = Path.Combine(RutaBase, "img/default.jpg");

                Bitmap bmp = await Task.Run(() =>
                {
                    string rutaFinal = File.Exists(ruta) ? ruta
                                     : File.Exists(rutaDefault) ? rutaDefault
                                     : null;

                    if (rutaFinal == null) return null;

                    byte[] bytes = File.ReadAllBytes(rutaFinal);

                    Bitmap original;
                    using (var ms = new MemoryStream(bytes))
                        original = new Bitmap(ms);

                    if (original.Width <= maxAncho && original.Height <= maxAlto)
                        return original;

                    float escalaX = (float)maxAncho / original.Width;
                    float escalaY = (float)maxAlto / original.Height;
                    float escala = Math.Min(escalaX, escalaY);

                    int nuevoAncho = (int)(original.Width * escala);
                    int nuevoAlto = (int)(original.Height * escala);

                    var redimensionada = new Bitmap(nuevoAncho, nuevoAlto);
                    using (var g = Graphics.FromImage(redimensionada))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.DrawImage(original, 0, 0, nuevoAncho, nuevoAlto);
                    }

                    original.Dispose();
                    return redimensionada;
                });

                if (IsDisposed || !IsHandleCreated)
                {
                    bmp?.Dispose();
                    return;
                }

                if (InvokeRequired)
                    Invoke(new Action(() => AsignarImagen(bmp)));
                else
                    AsignarImagen(bmp);
            }
            catch
            {
                if (!IsDisposed && IsHandleCreated)
                    img_icono.BackColor = Color.FromArgb(200, 200, 200);
            }
        }

        // ── Asigna liberando la imagen anterior ───────────────────
        private void AsignarImagen(Bitmap bmp)
        {
            if (IsDisposed) { bmp?.Dispose(); return; }

            if (img_icono.Image != null)
            {
                img_icono.Image.Dispose();
                img_icono.Image = null;
            }

            if (bmp != null)
            {
                img_icono.Image = bmp;
                img_icono.BackColor = Color.Transparent;
            }
        }

        // ── Constructor para Platillo ─────────────────────────────
        public alimento_card(Platillo alimento)
        {
            InitializeComponent();
            lbl_nombre.Text = alimento.Nombre;
            lbl_precio.Text = "$" + alimento.Precio.ToString("F2");
            txt_descripcion.Text = alimento.Descripcion;
            _imagenUrl = alimento.ImagenUrl;
            _id = alimento.PlatilloId;
            _tipo = "Platillo";
            _nombre = alimento.Nombre;
            _precio = alimento.Precio;
        }

        // ── Constructor para Bebida ───────────────────────────────
        public alimento_card(Bebida bebida)
        {
            InitializeComponent();
            lbl_nombre.Text = bebida.Nombre;
            lbl_precio.Text = "$" + bebida.Precio.ToString("F2") + " · " + bebida.Capacidad;
            txt_descripcion.Text = bebida.Descripcion;
            _imagenUrl = bebida.ImagenUrl;
            _id = bebida.BebidaId;
            _tipo = "Bebida";
            _nombre = bebida.Nombre;
            _precio = bebida.Precio;
        }

        // ── Constructor para Postre ───────────────────────────────
        public alimento_card(Postre postre)
        {
            InitializeComponent();
            lbl_nombre.Text = postre.Nombre;
            lbl_precio.Text = "$" + postre.Precio.ToString("F2");
            txt_descripcion.Text = postre.Descripcion;
            _imagenUrl = postre.ImagenUrl;
            _id = postre.PostreId;
            _tipo = "Postre";
            _nombre = postre.Nombre;
            _precio = postre.Precio;
        }

        // ── Botón agregar al pedido ───────────────────────────────
        private void btn_add_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            if (form1 == null) return;

            form1.PasarAlPedido(_id, _tipo, _nombre, _precio);
        }
    }
}