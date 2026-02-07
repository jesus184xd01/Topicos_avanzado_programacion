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
        private User user_act;
        public Reserva_form(User user)
        {
            InitializeComponent();
            this.user_act = user;
            CargarDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void CargarDatos()
        {
            var alojamientos = new[] {
                new {
                    Titulo = "Departamento acogedor cerca del metro",
                    Precio = "1400",
                    Imagenes = new Image[] {
                        Properties.Resources.departamento_0,
                        Properties.Resources.departamento_0_1,
                        Properties.Resources.departamento_0_2,
                        Properties.Resources.departamento_0_3,
                        Properties.Resources.departamento_0_4
                    },
                    Anfitrion = "Ricardo Vega"
                },
                new {
                    Titulo = "Departamento rústica en la playa",
                    Precio = "2300",
                    Imagenes = new Image[] {
                        Properties.Resources.departamento_1,
                        Properties.Resources.departamento_1_1,
                        Properties.Resources.departamento_1_2,
                        Properties.Resources.departamento_1_3,
                        Properties.Resources.departamento_1_4
                    },
                    Anfitrion = "María López"
                },
                new {
                    Titulo = "Habitación privada económica",
                    Precio = "850",
                    Imagenes = new Image[] {
                        Properties.Resources.departamento_2,
                        Properties.Resources.departamento_2_1,
                        Properties.Resources.departamento_2_2,
                        Properties.Resources.departamento_2_3,
                        Properties.Resources.departamento_2_4
                    },
                    Anfitrion = "Luis Navarro"
                },
                new {
                    Titulo = "Cabana de lujo",
                    Precio = "4800",
                    Imagenes = new Image[] {
                        Properties.Resources.departamento_3,
                        Properties.Resources.departamento_3_1,
                        Properties.Resources.departamento_3_2,
                        Properties.Resources.departamento_3_3,
                        Properties.Resources.departamento_3_4
                    },
                    Anfitrion = "Sofía Hernández"
                },
            };


            foreach (var datos in alojamientos)
            {
                ucTarjetaAlojamiento tarjeta = new ucTarjetaAlojamiento();
                tarjeta.Titulo = datos.Titulo;
                tarjeta.Precio = "$" + datos.Precio + " MXN";
                tarjeta.Imagen = datos.Imagenes[0];
                tarjeta.Tag = datos;

                tarjeta.TarjetaSeleccionada += (sender, e) =>
                {
                    var t = (ucTarjetaAlojamiento)sender;
                    var d = t.Tag as dynamic;

                    frmDetalleAlojamiento detalle = new frmDetalleAlojamiento(
                        user_act, d.Titulo, d.Precio, d.Imagenes, d.Anfitrion
                    );
                    detalle.ShowDialog();
                };

                flowLayoutPanel1.Controls.Add(tarjeta);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
