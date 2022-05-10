using Reservoom.WPF.Models;
using System.Threading.Tasks;

namespace Reservoom.WPF.ReservationConflictValidators
{
    public interface IReservationConflictValidator
    {
        Task<Reservation> GetConflictingReservation(Reservation reservation);
    }
}
