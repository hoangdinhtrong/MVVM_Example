using Microsoft.EntityFrameworkCore;
using Reservoom.WPF.DBContext;
using Reservoom.WPF.DTO;
using Reservoom.WPF.Models;
using Reservoom.WPF.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using(ReservoomDbContext db = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = await db.Reservations
                    .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r => r.EndTime > reservation.EndTime)
                    .Where(r => r.StartTime < reservation.StartTime)
                    .FirstOrDefaultAsync();
                if (reservationDTO == null)
                    return null;
                return ToReservation(reservationDTO);
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(
                new RoomID(dto.FloorNumber, dto.RoomNumber),
                dto.Username,
                dto.StartTime,
                dto.EndTime
            );
        }
    }
}
