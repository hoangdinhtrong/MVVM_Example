using Reservoom.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Stores
{
    public class HotelStore
    {
        public Hotel _hotel { get; }
        private readonly Lazy<Task> _initializeLazy;
        private readonly List<Reservation> _reservations;
        public IEnumerable<Reservation> Reservations => _reservations;

        public event Action<Reservation>? ReservationMade;

        public HotelStore(Hotel hotel)
        {
            _reservations = new List<Reservation>();
            _hotel = hotel;
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public Reservation? CreateUpdateRequest { get; set; }

        public async Task MakeReservation(Reservation reservation)
        {
            await _hotel.MakeReservation(reservation);
            _reservations.Add(reservation);
            OnReservationMade(reservation);
        }
        private void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }

        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
            _reservations.Clear();
            _reservations.AddRange(reservations);
        }
    }
}
