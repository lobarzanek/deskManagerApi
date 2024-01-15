using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IDeskRepository : IRepositoryBase<Desk>
    {
        Task<IEnumerable<Desk>> GetAllDesks();
        Task<IEnumerable<Desk>> GetAllDesksByRoomId(int id);
        Task<Desk> GetDeskById(int id);
        Task CreateDesk(Desk desk);
        void UpdateDesk(Desk desk);
        void DeleteDesk(Desk desk);
    }
}
