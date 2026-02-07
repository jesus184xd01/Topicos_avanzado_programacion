using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Reservación
{
    public partial class frmDetalleAlojamiento : Form
    {
        public frmDetalleAlojamiento(string titulo, string precio, System.Drawing.Image imagen, string anfitrion)
        {
            InitializeComponent();

            lblTituloDetalle.Text = titulo;
            lblPrecioDetalle.Text = precio;
            picImagenDetalle1.Image = imagen;
            lblAnfitrion.Text = "Anfitrión: " + anfitrion;
            

            
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnreservar_Click(object sender, EventArgs e)
        {
            // numericUpDown = numericUpDown1.Value; // almacenar el valor en la clase
            
            Voucher_form form = new Voucher_form();
                form.Show();
                this.Close();
        }
    }
}
