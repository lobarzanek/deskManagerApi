using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Repository.RepositoryModels
{
    public class BuildingRepository : RepositoryBase<Building>, IBuildingRepository
    {
        public BuildingRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateBuilding(Building building)
        {
            await Create(building);
        }

        public void DeleteBuilding(Building building)
        {
            Delete(building);
        }

        public async Task<IEnumerable<Building>> GetAllBuildings()
        {
            return await FindAll().OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<Building> GetBuildingById(int id)
        {
            return await FindByCondition(b => b.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateBuilding(Building building)
        {
            Update(building);
        }
    }
}
