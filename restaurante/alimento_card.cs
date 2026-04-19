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
    public partial class alimento_card : Form
    {
        public alimento_card()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // ── Constructor que recibe los datos del alimento ─────────
        public alimento_card(Alimento alimento)
        {
            InitializeComponent();

            // aquí asignas los datos a los controles que tengas en la card
            // cambia los nombres por los que tengas en tu diseñador
            lbl_nombre.Text = alimento.Nombre;
            lbl_precio.Text = "$" + alimento.Precio.ToString("F2");
            txt_descripcion.Text = alimento.Descripcion;

            // si tienes un PictureBox para la imagen:
            // if (alimento.Imagen != null)
            // {
            //     using (var ms = new System.IO.MemoryStream(alimento.Imagen))
            //         pictureBox1.Image = Image.FromStream(ms);
            // }
        }
    }
}
