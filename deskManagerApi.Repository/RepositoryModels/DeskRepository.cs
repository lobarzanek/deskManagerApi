using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
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
    public class DeskRepository : RepositoryBase<Desk>, IDeskRepository
    {
        public DeskRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateDesk(Desk desk)
        {
            await Create(desk);
        }

        public void DeleteDesk(Desk desk)
        {
            Delete(desk);
        }

        public async Task<IEnumerable<Desk>> GetAllDesks()
        {
            return await FindAll().OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<Desk> GetDeskById(int id)
        {
            return await FindByCondition(b => b.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateDesk(Desk desk)
        {
            Update(desk);
        }
    }
}
