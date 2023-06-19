using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;
using System.Linq.Expressions;

namespace Railway.Infrastructure.Data
{
    public class TrainRepository : ITrainRepository
    {
        private RailwayContext Context { get; }
        public TrainRepository(RailwayContext context)
        {
            Context = context;
        }
        public async Task Create(Train train)
        {
            await Context.Trains.AddAsync(train);
            await Context.SaveChangesAsync();
        }
        public async Task Update(Train train)
        {
            Context.Trains.Update(train);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Train train)
        {
            Context.Trains.Remove(train);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Train>> ListAll()
        {
            return await Context.Trains.Include(t=>t.Buillets).ToListAsync();
        }
        public async Task<Train> GetById(int id)
        {
            return await Context.Trains.Include(c => c.Buillets).FirstOrDefaultAsync(t=> t.Id == id);
        }

        public async Task<bool> Exists(int id)
        {
            return await Context.Trains.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> IsEmpty()
        {
            return Context.Trains == null;
        }

        public async Task<List<Train>> GetList(Expression<Func<Train, bool>> criteria)
        {
            return await Context.Trains.Include(t=>t.Buillets).Where(criteria).ToListAsync();
        } 

        
    }
}
