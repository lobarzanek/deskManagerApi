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
    public class FloorRepository : RepositoryBase<Floor>, IFloorRepository
    {
        public FloorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateFloor(Floor floor)
        {
            await Create(floor);
        }

        public void DeleteFloor(Floor floor)
        {
            Delete(floor);
        }

        public async Task<IEnumerable<Floor>> GetAllFloors()
        {
            return await FindAll().OrderBy(f => f.Name).ToListAsync();
        }

        public async Task<Floor> GetFloorById(int id)
        {
            return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateFloor(Floor floor)
        {
            Update(floor);
        }
    }
}
