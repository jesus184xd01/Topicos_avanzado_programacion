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
    public partial class ucTarjetaAlojamiento : UserControl
    {
        public ucTarjetaAlojamiento()
        {
            InitializeComponent();
        }

        // Propiedad para cambiar el Titulo desde fuera
        public string Titulo
        {
            get { return lblUbicacion.Text; }
            set { lblUbicacion.Text = value; }
        }

        // Propiedad para cambiar el precio
        public string Precio
        {
            get { return lblPrecio.Text; }
            set { lblPrecio.Text = value; }
        }

        //Propiedad para cambiar imagen
        public  Image Imagen
        {
            get { return picAlojamiento.Image; }
            set { picAlojamiento.Image = value; }
        }
    }
}
