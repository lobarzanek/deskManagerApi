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
        Task Save();
    }
}
