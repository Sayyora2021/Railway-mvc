using Railway.Core.Entities;
using System.Linq.Expressions;

namespace Railway.Core.Seedwork
{
    public interface IDestinationRepository
    {
        Task Create(Destination destination);
        Task Delete(Destination destination);
        Task<Destination> GetById(int id);
        Task<List<Destination>> ListAll();
        Task Update(Destination destination);
        Task<bool> Exists(int id);
        Task<bool> IsEmpty();
        Task<List<Destination>> GetList(Expression<Func<Destination, bool>> criteria);
    }
}
