using Reservoom.WPF.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetReservationsForUser(string userName)
        {
            return _reservations.Where(r => r.Username == userName);
        }

        public void AddReservation(Reservation reservation)
        {
            foreach(Reservation item in _reservations)
            {
                if (item.Conflicts(reservation))
                {
                    throw new ReservationConflictException(item, reservation);
                }
            }

            _reservations.Add(reservation);
        }
    }
}
