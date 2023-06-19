using Railway.Core.Entities;

namespace Railway.Core.Seedwork
{
    public interface IGareRepository
    {
        Task Create(Gare gare);
        Task Delete(Gare gare);
        Task<bool> Exists(int id);
        Task<Gare> GetById(int id);
        Task<bool> IsEmpty();
        Task<List<Gare>> ListAll();
        Task Update(Gare gare);
    }
}
