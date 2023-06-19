using Railway.Core.Seedwork;
using Railway.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Railway.Infrastructure.Data
{
    public class PassagerRepository : IPassagerRepository
    {
        private RailwayContext Context { get; }
        public PassagerRepository(RailwayContext context)
        {
            Context = context;
        }
        public async Task Create(Passager passager)
        {
            await Context.Passagers.AddAsync(passager);
            await Context.SaveChangesAsync();
         
        }
        public async Task Update(Passager passager)
        {
            Context.Passagers.Update(passager);
            await Context.SaveChangesAsync();

        }
        public async Task Delete(Passager passager)
        {
            Context.Passagers.Remove(passager);
            await Context.SaveChangesAsync();

        }
        public async Task<List<Passager>> ListAll()
        {
            return await Context.Passagers.Include(t => t.ListReservation).ToListAsync();
        }
        public async Task<Passager> GetById(int id)
        {
            return await Context.Passagers.Include(t => t.ListReservation).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> Exists(int id)
        {
            return await Context.Passagers.Include(t => t.ListReservation).AnyAsync(c => c.Id == id);
        }
        public async Task<bool> IsEmpty()
        {
            return Context.Passagers == null;
        }
    }
}
