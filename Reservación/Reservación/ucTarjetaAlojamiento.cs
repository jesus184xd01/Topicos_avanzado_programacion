using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D; // Necesario para el diseño moderno
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservación
{
    public partial class ucTarjetaAlojamiento : UserControl
    {
        private bool isHovered = false;
        private int borderRadius = 20;

        public ucTarjetaAlojamiento()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Evita el parpadeo al rediseñar
            this.BackColor = Color.White;
            AsignarEventosRecursivos(this);
        }

        private void AsignarEventosRecursivos(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                c.MouseEnter += (s, e) => MouseEntra();
                c.MouseLeave += (s, e) => MouseSale();
                c.MouseClick += (s, e) => OnMouseClick(e);

                if (c.HasChildren)
                    AsignarEventosRecursivos(c);
            }
            // También asignar al control principal
            this.MouseEnter += (s, e) => MouseEntra();
            this.MouseLeave += (s, e) => MouseSale();
        }

        private void MouseEntra()
        {
            isHovered = true;
            this.BackColor = Color.FromArgb(248, 248, 248);
            this.Cursor = Cursors.Hand;
            this.Invalidate(); // Fuerza a que se ejecute OnPaint
        }

        private void MouseSale()
        {
            isHovered = false;
            this.BackColor = Color.White;
            this.Cursor = Cursors.Default;
            this.Invalidate(); // Fuerza a que se ejecute OnPaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Crear el camino redondeado para toda la tarjeta
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            GraphicsPath path = GetRoundedPath(rect, borderRadius);

            // 2. Aplicar la forma al control (Esto corta las esquinas de la imagen automáticamente)
            this.Region = new Region(path);

            // 3. Dibujar el borde sutil
            Color colorBorde = isHovered ? Color.DarkGray : Color.FromArgb(220, 220, 220);
            using (Pen p = new Pen(colorBorde, 1))
            {
                e.Graphics.DrawPath(p, path);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // --- EVENTO DE SELECCIÓN ---
        public event EventHandler TarjetaSeleccionada;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            TarjetaSeleccionada?.Invoke(this, e);
        }

        // --- PROPIEDADES ---
        public string Titulo
        {
            get { return lblUbicacion.Text; }
            set { lblUbicacion.Text = value; }
        }

        public string Precio
        {
            get { return lblPrecio.Text; }
            set { lblPrecio.Text = value; }
        }

        public Image Imagen
        {
            get { return picAlojamiento.Image; }
            set { picAlojamiento.Image = value; }
        }

        public object DatosPropiedad { get; set; }
    }
}