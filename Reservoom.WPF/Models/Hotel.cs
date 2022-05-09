﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public Hotel(string name)
        {
            Name = name;
            _reservationBook = new ReservationBook();
        }

        public string Name { get; set; }

        public IEnumerable<Reservation> GetReservationsForUser(string username)
        {
            return _reservationBook.GetReservationsForUser(username);
        }

        public void MakeReservation(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);
        }
    }
}
