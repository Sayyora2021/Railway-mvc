using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;

namespace Railway.Infrastructure.Data
{
    public class ExemplaireRepository : IExemplaireRepository
    {
        private RailwayContext Context { get;}
        public ExemplaireRepository(RailwayContext context)
        {
            Context = context;
        }
        public async Task Create(Exemplaire exemplaire)
        {
            await Context.Exemplaires.AddAsync(exemplaire);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Exemplaire exemplaire)
        {
            Context.Exemplaires.AddAsync(exemplaire);
            await Context.SaveChangesAsync();
        }
        public async Task Delete(Exemplaire exemplaire)
        {
            Context.Exemplaires.Remove(exemplaire);
            await Context.SaveChangesAsync();
        }
        public async Task<List<Exemplaire>> ListAll()
        {
            return await Context.Exemplaires.Include(t => t.Buillet).ToListAsync();

        }
        public async Task<Exemplaire> GetById(int id)
        {
            return await Context.Exemplaires.Include(e => e.Buillet).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<bool> Exists(int id)
        {
            return await Context.Exemplaires.AnyAsync(c => c.Id == id);
        }
        public async Task<bool> IsEmpty()
        {
            return Context.Exemplaires == null;
        }

       
    }
}
