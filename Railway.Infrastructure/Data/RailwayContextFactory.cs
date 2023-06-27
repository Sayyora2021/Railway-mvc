using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Railway.Infrastructure.Data
{
    public class RailwayContextFactory : IDesignTimeDbContextFactory<RailwayContext>
    {
        public RailwayContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RailwayContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Railway-new;Integrated Security=True");
            return new RailwayContext(optionsBuilder.Options);

            
        }
    }
}
