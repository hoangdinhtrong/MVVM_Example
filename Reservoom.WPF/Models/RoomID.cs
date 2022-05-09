using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Models
{
    public class RoomID
    {
        public RoomID(int? floorNumber, int? roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public int? FloorNumber { get; set; }

        public int? RoomNumber { get; set; }

        public override bool Equals(object? obj)
        {
            return  obj is RoomID other && 
                FloorNumber == other.FloorNumber &&
                RoomNumber == other.RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public override string ToString()
        {
            return $"{FloorNumber}:{RoomNumber}";
        }

        public static bool operator ==(RoomID item1, RoomID item2)
        {
            if (item1 is null && item2 is null)
                return true;

            return !(item1 is null) && item1.Equals(item2);
        }

        public static bool operator !=(RoomID item1, RoomID item2)
        {
            return !(item1 == item2);
        }
    }
}
