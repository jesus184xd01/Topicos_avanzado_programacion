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
    public partial class Log_in_form : Form
    {
        public Log_in_form()
        {
            InitializeComponent();
        }

        // cambio del form de log in al de reservacion
        private void button1_Click(object sender, EventArgs e)
        {

            Reserva_form form = new Reserva_form();

            form.Show();
            this.Hide();
        }
    }
}
