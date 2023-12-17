using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IFloorRepository : IRepositoryBase<Floor>
    {
        Task<IEnumerable<Floor>> GetAllFloors();
        Task<Floor> GetFloorById(int id);
        Task CreateFloor(Floor floor);
        void UpdateFloor(Floor floor);
        void DeleteFloor(Floor floor);
    }
}
