using deskManagerApi.Contracts;
using deskManagerApi.Entities;
using deskManagerApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Repository
{

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _context;
        private IBrandRepository _brand;
        public IBrandRepository Brand
        {
            get
            {
                if (_brand == null)
                {
                    _brand = new BrandRepository(_context);
                }
                return _brand;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
