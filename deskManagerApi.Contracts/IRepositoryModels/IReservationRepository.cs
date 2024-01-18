using deskManagerApi.Entities.Models;
using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllReservations();
        Task<IEnumerable<Reservation>> GetReservationsByUserId(int id);
        Task<Reservation> GetReservationById(int id);
        Task CreateReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
    }
}
