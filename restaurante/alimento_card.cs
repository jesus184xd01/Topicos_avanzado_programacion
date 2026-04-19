using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace restaurante
{
    public partial class alimento_card : Form
    {
        private static readonly string RutaBase =
            AppDomain.CurrentDomain.BaseDirectory;

        public alimento_card()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        // ── Método para cargar imagen ─────────────────────────────
        private void CargarImagen(string imagenUrl)
        {
            try
            {
                string ruta = Path.Combine(RutaBase, imagenUrl ?? "img/default.jpg");

                if (File.Exists(ruta))
                    img_icono.Image = Image.FromFile(ruta);
                else if (File.Exists(Path.Combine(RutaBase, "img/default.jpg")))
                    img_icono.Image = Image.FromFile(Path.Combine(RutaBase, "img/default.jpg"));
                else
                    img_icono.Image = null;
            }
            catch
            {
                img_icono.Image = null;
            }

            img_icono.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Constructor para Platillo
        public alimento_card(Platillo alimento)
        {
            InitializeComponent();
            lbl_nombre.Text = alimento.Nombre;
            lbl_precio.Text = "$" + alimento.Precio.ToString("F2");
            txt_descripcion.Text = alimento.Descripcion;
            CargarImagen(alimento.ImagenUrl);
        }

        // Constructor para Postre
        public alimento_card(Postre postre)
        {
            InitializeComponent();
            lbl_nombre.Text = postre.Nombre;
            lbl_precio.Text = "$" + postre.Precio.ToString("F2");
            txt_descripcion.Text = postre.Descripcion;
            CargarImagen(postre.ImagenUrl);
        }

        // Constructor para Bebida
        public alimento_card(Bebida bebida)
        {
            InitializeComponent();
            lbl_nombre.Text = bebida.Nombre;
            lbl_precio.Text = "$" + bebida.Precio.ToString("F2") + " · " + bebida.Capacidad;
            txt_descripcion.Text = bebida.Descripcion;
            CargarImagen(bebida.ImagenUrl);
        }
    }
}