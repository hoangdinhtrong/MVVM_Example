using Microsoft.EntityFrameworkCore;
using Reservoom.WPF.DTO;

namespace Reservoom.WPF.DBContext
{
    public class ReservoomDbContext : DbContext
    {
        public ReservoomDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ReservationDTO> Reservations { get; set; } = null!;


    }
}
