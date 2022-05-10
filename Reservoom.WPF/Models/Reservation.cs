using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Models
{
    public class Reservation
    {
        public Reservation(RoomID roomID, string username, DateTime startTime, DateTime endTime)
        {
            RoomID = roomID;
            StartTime = startTime;
            EndTime = endTime;
            Username = username;
        }

        public RoomID RoomID { get; set; }

        public string Username { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan Length => EndTime.Subtract(StartTime);

        public bool Conflicts(Reservation reservation)
        {
            if(reservation.RoomID != RoomID)
            {
                return false;
            }
            return reservation.StartTime < EndTime || reservation.EndTime > StartTime;
        }
    }
}
