using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;


namespace Railway.Infrastructure.Data
{
    public class GareRepository : IGareRepository
    {
        private RailwayContext Context { get; }
        public GareRepository(RailwayContext context)
        {
            Context = context;
        }
        public async Task Create(Gare gare)
        {
            await Context.Gares.AddAsync(gare);
            await Context.SaveChangesAsync();
        }
        public async Task Update(Gare gare)
        {
            Context.Gares.Update(gare);
            await Context.SaveChangesAsync();

        }
        public async Task Delete(Gare gare)
        {
            Context.Gares.Remove(gare);
            await Context.SaveChangesAsync();

        }

        public async Task<List<Gare>> ListAll()
        {
            return await Context.Gares.ToListAsync();
        }
        public async Task<Gare> GetById(int id)
        {
            return await Context.Gares.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<bool> Exists(int id)
        {
            return await Context.Gares.AnyAsync(c => c.Id == id);
        }
        public async Task<bool> IsEmpty()
        {
            return Context.Gares == null;
        }

       
    }
}
