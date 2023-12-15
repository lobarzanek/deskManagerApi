using deskManagerApi.Contracts;
using deskManagerApi.Models;

namespace deskManagerApi.IRepository
{
    public interface IBrandRepository : IRepositoryBase<Brand>
    {
        Task<IEnumerable<Brand>> GetAllBrands();
        Task<Brand> GetBrandById(int id);
        Task CreateBrand(Brand brand);
        void UpdateBrand(Brand brand);
        void DeleteBrand(Brand brand);
    }
}
