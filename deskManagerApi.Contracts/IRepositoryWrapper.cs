using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts
{
    public interface IRepositoryWrapper
    {
        IBrandRepository Brand { get; }
        IBuildingRepository Building { get; }
        IDeskRepository Desk { get; }
        IFloorRepository Floor { get; }
        Task Save();
    }
}
