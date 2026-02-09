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
        private Form formulario_padre; // ← Guardar referencia
        private User usuario_act;
        private string titulo_alojamiento;
        private string nombre_anfitrion;
        private decimal precio_base;
        public frmDetalleAlojamiento(Form padre, User usuario, string titulo, string precio, System.Drawing.Image[] imagenes, string anfitrion)
        {
            InitializeComponent();

            // Guardar datos
            formulario_padre = padre;
            usuario_act = usuario;
            titulo_alojamiento = titulo;
            nombre_anfitrion = anfitrion;
            precio_base = decimal.Parse(precio);

            lblTituloDetalle.Text = titulo;
            lblPrecioDetalle.Text = "$" + precio + " MXN";
            picImagenDetalle1.Image = imagenes[0];
            picImagenDetalle2.Image = imagenes[1];
            picImagenDetalle3.Image = imagenes[2];
            picImagenDetalle4.Image = imagenes[3];
            picImagenDetalle5.Image = imagenes[4];

            lblAnfitrion.Text = "Anfitrión: " + anfitrion;
            

            
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnreservar_Click(object sender, EventArgs e)
        {
            // Crear objeto Reservation simple
            Reservation reservacion = new Reservation();

            // Llenar datos con .set
            reservacion.Client = usuario_act;
            reservacion.checkIn = monthCalendar1.SelectionStart;
            reservacion.checkOut = monthCalendar1.SelectionEnd;
            reservacion.nights = (monthCalendar1.SelectionEnd - monthCalendar1.SelectionStart).Days + 1;
            reservacion.guests = (int)numericUpDown1.Value;
            reservacion.host = nombre_anfitrion;
            reservacion.total = precio_base + 100 * reservacion.nights;
            reservacion.housing_name = titulo_alojamiento;

            formulario_padre?.Close(); // ← Cerrar el formulario padre
            // Pasar reservación al voucher
            Voucher_form form = new Voucher_form(reservacion);
            form.Show();
            this.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Crear objeto Reservation simple
            Reservation reservacion = new Reservation();

            DateTime fechaInicio = e.Start;
            DateTime fechaFin = e.End;
            int diasSeleccionados = (fechaFin - fechaInicio).Days + 1;
            lbl_noches.Text = $"{diasSeleccionados} noches";
            lblPrecioDetalle.Text = $"${precio_base + 100 * diasSeleccionados} MXN";
        }
    }
}
