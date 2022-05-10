using Reservoom.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private readonly Reservation _reservation;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }

        public string? RoomID => _reservation.RoomID?.ToString();

        public int? FloorNumber => _reservation.RoomID.FloorNumber;

        public int? RoomNumber => _reservation.RoomID.RoomNumber;

        public string Username => _reservation.Username;

        public string StartDate => _reservation.StartTime.ToString("d");

        public string EndDate => _reservation.EndTime.ToString("d");
    }
}
