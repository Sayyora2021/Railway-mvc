using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;

namespace Railway.Infrastructure.Data
{
    public class ReservationRepository: IReservationRepository
    {
        private RailwayContext Context { get;}
        public ReservationRepository(RailwayContext context)
        {
            Context = context;
        }
        public async Task AddBuilletToPassager(Reservation reservation)
        {
            await Context.Reservations.AddAsync(reservation);
            await Context.SaveChangesAsync();
        }
        
        public async Task RemoveBuilletFromPassager(Reservation reservation)
        {
            Context.Reservations.Remove(reservation);
            await Context.SaveChangesAsync();
        }
        public async Task<List<Reservation>> ListAllBookTaken(int passagerId)
        {

            return await Context.Reservations.Where(e => e.Passager.Id == passagerId).ToListAsync();

        }
        public async Task<Reservation> GetById(int id)
        {
            return await Context.Reservations.Include(e => e.Exemplaire).Include(e => e.Passager).FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task Create(Reservation reservation)
        {
            Context.Reservations.Add(reservation);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> ListAll()
        {
            return await Context.Reservations.Include(c => c.Passager).Include(c => c.Exemplaire).Include(c => c.Exemplaire.Buillet).ToListAsync();
        }

        public async Task Update(Reservation reservation)
        {
            Context.Reservations.Update(reservation);
            await Context.SaveChangesAsync();
        }
        
    }
}
