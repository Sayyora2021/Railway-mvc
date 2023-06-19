using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;
using System.Linq.Expressions;

namespace Railway.Infrastructure.Data
{
    public class BuilletRepository: IBuilletRepository
    {
        private RailwayContext Context { get;}
        private IDestinationRepository DestinationRepository { get; }
        private ITrainRepository TrainRepository { get; }

        public BuilletRepository(RailwayContext context, IDestinationRepository destinationRepository, ITrainRepository trainRepository)
        {
            Context = context;
            DestinationRepository = destinationRepository;
            TrainRepository = trainRepository;
        }
        public async Task Create(Buillet buillet)
        {
            await Context.Buillets.AddAsync(buillet);
            await Context.SaveChangesAsync();
        }
        public async Task Update(Buillet buillet, int[] destination, int[]train)
        {
            buillet.Destinations.Clear();
            buillet.Trains.Clear();
            Context.Update(buillet);
            await Context.SaveChangesAsync();
        }
        public async Task Update(Buillet buillet)
        {
            Buillet builletOld = await GetById(buillet.Id);
            Context.Buillets.Remove(builletOld);
            Context.Buillets.Update(buillet);
            await Context.SaveChangesAsync();

        }
        public async Task Delete(Buillet buillet)
        {
            Context.Buillets.Remove(buillet);
            await Context.SaveChangesAsync();

        }
        public async Task<List<Buillet>> ListAll()
        {
            return await Context.Buillets.Include(c => c.Destinations).Include(c => c.Trains).ToListAsync();
        }
        public async Task<Buillet> GetById(int id)
        {
            return await Context.Buillets.Include(c => c.Destinations).Include(c => c.Trains).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<bool> Exists(int id)
        {
            return await Context.Buillets.AnyAsync(c => c.Id == id);
        }
        public async Task<bool> IsEmpty()
        {
            return Context.Buillets == null;
        }
        public async Task<List<Buillet>> GetList(Expression<Func<Buillet, bool>> criteria)
        {
            return await Context.Buillets.Include(c => c.Destinations).Include(c => c.Trains).Where(criteria).ToListAsync();
        }

       
    }
}
