using Railway.Core.Entities;

namespace Railway.Core.Seedwork
{
    public interface IReservationRepository
    {
        Task AddBuilletToPassager(Reservation reservation);
        Task<Reservation> GetById(int id);
        Task<List<Reservation>> ListAllBookTaken(int lecteurId);
        Task RemoveBuilletFromPassager(Reservation reservation);

        Task Create(Reservation reservation);
        Task Update(Reservation reservation);

        Task<IEnumerable<Reservation>> ListAll();
    }
}
