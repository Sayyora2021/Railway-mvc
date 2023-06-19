using Railway.Core.Entities;

namespace Railway.Core.Seedwork
{
    public interface IExemplaireRepository
    {
        Task Create(Exemplaire exemplaire);
        Task Delete(Exemplaire exemplaire);
        Task<Exemplaire> GetById(int id);
        Task<List<Exemplaire>> ListAll();
        Task Update(Exemplaire exemplaire);
        Task<bool> Exists(int id);
        Task<bool> IsEmpty();
    }
}
