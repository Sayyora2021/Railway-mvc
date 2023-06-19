using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;

namespace Railway.Infrastructure.Data
{
    public class RailwayContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RailwayContext(DbContextOptions<RailwayContext> options) : base(options)
        {
        }
        public DbSet<Passager> Passagers => Set<Passager>();
        public DbSet<Destination> Destinations => Set<Destination>();
        public DbSet<Exemplaire> Exemplaires => Set<Exemplaire>();
        public DbSet<Buillet> Buillets => Set<Buillet>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Gare> Gares => Set<Gare>();
        public DbSet<Train> Trains => Set<Train>();

        

    }
}
