using Reservoom.WPF.DBContext;
using Reservoom.WPF.DTO;
using Reservoom.WPF.Models;
using Reservoom.WPF.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            using (ReservoomDbContext db = _dbContextFactory.CreateDbContext()){
                ReservationDTO reservationDTO = ToReservationDTO(reservation);

                db.Reservations.Add(reservationDTO);
                await db.SaveChangesAsync();
            }
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = Convert.ToInt32(reservation.RoomID.FloorNumber),
                RoomNumber = Convert.ToInt32(reservation.RoomID.RoomNumber),
                Username = reservation.Username,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime
            };
        }
    }
}
