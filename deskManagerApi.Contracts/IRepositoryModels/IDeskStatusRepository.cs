using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IDeskStatusRepository : IRepositoryBase<DeskStatus>
    {
        Task<IEnumerable<DeskStatus>> GetAllDeskStatuses();
        Task<DeskStatus> GetDeskStatusById(int id);
        Task CreateDeskStatus(DeskStatus status);
        void UpdateDeskStatus(DeskStatus status);
        void DeleteDeskStatus(DeskStatus status);
    }
}
