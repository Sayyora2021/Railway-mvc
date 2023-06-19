using Railway.Core.Entities;
using System.Linq.Expressions;

namespace Railway.Core.Seedwork
{
    public interface ITrainRepository
    {
        Task Create(Train train);
        Task Delete(Train train);
        Task<Train> GetById(int id);
        Task<List<Train>> ListAll();
        Task Update(Train train);
        Task<bool> Exists(int id);
        Task<bool> IsEmpty();
        Task<List<Train>> GetList(Expression<Func<Train, bool>> criteria);
    }
}
