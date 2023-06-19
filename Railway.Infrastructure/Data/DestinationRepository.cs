using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;
using System.Linq.Expressions;

namespace Railway.Infrastructure.Data
{
    public class DestinationRepository: IDestinationRepository
    {
        private RailwayContext Context { get; }

        public DestinationRepository (RailwayContext context)
        {
            Context= context;
        }
        public async Task Create(Destination destination)
        {
            await Context.Destinations.AddAsync(destination);
            await Context.SaveChangesAsync();
        }
        public async Task Update(Destination destination)
        {
            Context.Destinations.Update(destination);
            await Context.SaveChangesAsync();

        }
        public async Task Delete(Destination destination)
        {
            Context.Destinations.Remove(destination);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Destination>> ListAll()
        {
           return await Context.Destinations.Include(t=>t.Buillets).ToListAsync();
        }

        public async Task<Destination> GetById(int id)
        {
            return await Context.Destinations.Include(c=>c.Buillets).FirstOrDefaultAsync(c=>c.Id == id);
        }
        public async Task<bool> Exists(int id)
        {
            return await Context.Destinations.AnyAsync(c=>c.Id ==id);
        }

        public async Task<bool> IsEmpty()
        {
            return Context.Destinations == null;
        }

        public async Task<List<Destination>> GetList(Expression<Func<Destination, bool>> criteria)
        {
            return await Context.Destinations.Include(t=>t.Buillets).Where(criteria).ToListAsync();
        }

        
        }
    }

