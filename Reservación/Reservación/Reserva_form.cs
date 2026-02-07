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
            for (int i = 0; i < 5; i++)
            {
                ucTarjetaAlojamiento tarjeta = new ucTarjetaAlojamiento();
                tarjeta.Titulo = "Apartamento moderno #" + i;
                tarjeta.Precio = "$1,500 MXN";
                tarjeta.Imagen = Properties.Resources.depa1;

                // EL MOMENTO CLAVE:
                tarjeta.TarjetaSeleccionada += (sender, e) =>
                {
                    // Recuperamos la tarjeta que fue clickeada
                    ucTarjetaAlojamiento t = (ucTarjetaAlojamiento)sender;

                    // Abrimos el nuevo form pasando los datos de esa tarjeta
                    // Nota: Aquí podrías pasar un nombre de anfitrión desde una base de datos
                    frmDetalleAlojamiento detalle = new frmDetalleAlojamiento(
                        t.Titulo,
                        t.Precio,
                        t.Imagen,
                        
                        "Juan Pérez" // Aquí iría el dato del anfitrión
                    );

                    detalle.ShowDialog(); // ShowDialog lo abre como ventana modal
                };

                flowLayoutPanel1.Controls.Add(tarjeta);
            }
        }
    }
}
