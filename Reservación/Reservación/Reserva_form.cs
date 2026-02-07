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
            // Supongamos que tienes una lista o un bucle
            var alojamientos = new[] {
                new { Titulo = "Departamento acogedor cerca del metro", Precio = "$1,400 MXN", Imagen = Properties.Resources.departamento_0, Anfitrion = "Ricardo Vega" },
                new { Titulo = "Departamento rústica en la playa", Precio = "$2,300 MXN", Imagen = Properties.Resources.departamento_1, Anfitrion = "María López" },
                new { Titulo = "Habitación privada económica", Precio = "$850 MXN", Imagen = Properties.Resources.departamento_2, Anfitrion = "Luis Navarro" },
                new { Titulo = "Cabana de lujo", Precio = "$4,800 MXN", Imagen = Properties.Resources.departamento_3, Anfitrion = "Sofía Hernández" },
            };


            foreach (var datos in alojamientos)
            {
                ucTarjetaAlojamiento tarjeta = new ucTarjetaAlojamiento();
                tarjeta.Titulo = datos.Titulo;
                tarjeta.Precio = datos.Precio;
                tarjeta.Imagen = datos.Imagen;
                tarjeta.Tag = datos;

                tarjeta.TarjetaSeleccionada += (sender, e) =>
                {
                    var t = (ucTarjetaAlojamiento)sender;
                    var d = t.Tag as dynamic;

                    frmDetalleAlojamiento detalle = new frmDetalleAlojamiento(
                        d.Titulo, d.Precio, d.Imagen, d.Anfitrion
                    );
                    detalle.ShowDialog();
                };

                flowLayoutPanel1.Controls.Add(tarjeta);
            }
        }
    }
}
