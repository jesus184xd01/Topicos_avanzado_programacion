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
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string PlaceType { get; set; }
        public int Guests { get; set; }
        public decimal Total { get; set; }
    }
}
