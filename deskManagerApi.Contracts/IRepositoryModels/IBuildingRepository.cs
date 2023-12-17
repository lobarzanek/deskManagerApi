#region Usings

using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IBuildingRepository : IRepositoryBase<Building>
    {
        Task<IEnumerable<Building>> GetAllBuildings();
        Task<Building> GetBuildingById(int id);
        Task CreateBuilding(Building building);
        void UpdateBuilding(Building building);
        void DeleteBuilding(Building building);
    }
}
