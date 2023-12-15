using deskManagerApi.Entities;
using deskManagerApi.Models;
using deskManagerApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace deskManagerApi.IRepository
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateBrand(Brand brand)
        {
            await Create(brand);
        }

        public void DeleteBrand(Brand brand)
        {
            Delete(brand);
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await FindAll().OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<Brand> GetBrandById(int id)
        {
            return await FindByCondition(b => b.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateBrand(Brand brand)
        {
            Update(brand);
        }
    }
}
