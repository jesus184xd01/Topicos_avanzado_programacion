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
    public partial class Voucher_form : Form
    {
        private Reservation reservacion;
        public Voucher_form(Reservation reserva)
        {
            InitializeComponent();
            this.reservacion = reserva;
        }
    }
}
