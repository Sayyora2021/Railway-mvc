using Railway.Core.Entities;
using System.Linq.Expressions;


    namespace Railway.Core.Seedwork
    {
        public interface IBuilletRepository
        {
            Task Create(Buillet buillet);
            Task Delete(Buillet buillet);
            Task<Buillet> GetById(int id);
            Task<List<Buillet>> ListAll();

            Task Update(Buillet buillet, int[] destinations, int[] train);
            Task Update(Buillet buillet);

            Task<bool> Exists(int id);
            Task<bool> IsEmpty();
            Task<List<Buillet>> GetList(Expression<Func<Buillet, bool>> criteria);
        }
    }
