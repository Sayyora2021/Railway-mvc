using Railway.Core.Entities;

namespace Railway.Core.Seedwork
{
    public interface IPassagerRepository
    {
        Task Create(Passager passager);
        Task Delete(Passager passager);
        Task<bool> Exists(int id);
        Task<Passager> GetById(int id);
        Task<bool> IsEmpty();
        Task<List<Passager>> ListAll();
        Task Update(Passager passager);
    }
}
