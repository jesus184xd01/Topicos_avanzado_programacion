using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurante
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_desayuno_Click(object sender, EventArgs e)
        {

        }

        private void btn_almuerzo_Click(object sender, EventArgs e)
        {

        }

        private void btn_comida_Click(object sender, EventArgs e)
        {

        }

        private void btn_cena_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel_fondo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void settings(object sender, EventArgs e)
        {
            //panel contenedor
            panel_container.Location = new Point((this.panel_fondo.Width - panel_container.Width) / 2, panel_container.Location.Y);

            //forms principal
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            //boton titulo
            title_lbl.Location = new Point((this.panel_fondo.Width - title_lbl.Width) / 2, title_lbl.Location.Y + 10);

            //botones categorias
            int espaciado = 50; //gap
            int anchoBotones = (btn_desayuno.Width * 4) + (espaciado * 3); // ancho total de los 4 + espacios
            int startX = title_lbl.Location.X + (title_lbl.Width / 2) - (anchoBotones / 2); // punto de inicio centrado
            int posY = title_lbl.Location.Y + title_lbl.Height + 10; // debajo del título

            btn_desayuno.Location = new Point(startX, posY);
            btn_almuerzo.Location = new Point(startX + btn_desayuno.Width + espaciado, posY);
            btn_comida.Location = new Point(startX + (btn_desayuno.Width + espaciado) * 2, posY);
            btn_cena.Location = new Point(startX + (btn_desayuno.Width + espaciado) * 3, posY);

            //boton administrar
            btn_administrar.Location = new Point(this.panel_fondo.Width - btn_administrar.Width - 20, this.panel_fondo.Height - btn_administrar.Height - 20);

        }

        private void CargarPlatillos(List<Platillo> platillos)
        {
            panel_contenedor.Controls.Clear(); // limpiar antes de cargar

            foreach (var platillo in platillos)
            {
                CardForm card = new CardForm(platillo); // le pasas los datos
                card.TopLevel = false;
                card.FormBorderStyle = FormBorderStyle.None;
                card.Visible = true;
                panel_contenedor.Controls.Add(card);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settings(sender, e);
        }
    }
}
