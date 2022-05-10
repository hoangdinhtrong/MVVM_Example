using Microsoft.EntityFrameworkCore;
using Reservoom.WPF.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.WPF.Services.ReservationProviders
{
    public class ReservoomDbContextFactory
    {
        private readonly string _connectionString;

        public ReservoomDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReservoomDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlServer(_connectionString)
                .Options;
            return new ReservoomDbContext(options);
        }
    }
}
