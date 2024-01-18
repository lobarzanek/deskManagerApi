using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.Entities.Models;
using deskManagerApi.IRepository;
using deskManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Repository.RepositoryModels
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateReservation(Reservation reservation)
        {
            await Create(reservation);
        }

        public void DeleteReservation(Reservation reservation)
        {
            Delete(reservation);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await FindAll().OrderBy(b => b.Id).ToListAsync();
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            return await FindByCondition(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserId(int id)
        {
            return await FindAll().Where(r => r.UserId == id).OrderByDescending(b => b.Id).ToListAsync();
        }

        public void UpdateReservation(Reservation reservation)
        {
            Update(reservation);
        }
    }
}
