using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservación
{
    public partial class Reserva_form : Form
    {
        public Reserva_form()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //label4.Text = "Reservación hecha para " + textBox1.Text + " " + textBox2.Text + " el día " + monthCalendar1.SelectionStart.ToShortDateString() + " en la habitación " + textBox3.Text;

            // redireccion hacia el voucher de la reservacion
            Voucher_form form = new Voucher_form();
            form.Show();
            this.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void CargarDatos()
        {
            for(int i = 0; i < 6; i++)
            {
                ucTarjetaAlojamiento tarjeta = new ucTarjetaAlojamiento();
                tarjeta.Titulo = "Departamento en CDMX";
                tarjeta.Precio = "$2000 MNX";
                tarjeta.Margin = new Padding(15); //Espacio de separacion entre las tarjetas

                flowLayoutPanel1.Controls.Add(tarjeta);
            }
        }
    }
}
