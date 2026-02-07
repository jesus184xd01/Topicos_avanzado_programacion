using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservación
{
    public class Reservation
    {
        public User Client { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public int nights { get; set; }
        public int guests { get; set; }
        public decimal total { get; set; }
        public string host { get; set; }
        public string housing_name { get; set; }
    }
}
