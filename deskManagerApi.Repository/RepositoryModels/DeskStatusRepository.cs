using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.Models;
using deskManagerApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace deskManagerApi.IRepository
{
    public class DeskStatusRepository : RepositoryBase<DeskStatus>, IDeskStatusRepository
    {
        public DeskStatusRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateDeskStatus(DeskStatus status)
        {
            await Create(status);
        }

        public void DeleteDeskStatus(DeskStatus status)
        {
            Delete(status);
        }

        public async Task<IEnumerable<DeskStatus>> GetAllDeskStatuses()
        {
            return await FindAll().OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<DeskStatus> GetDeskStatusById(int id)
        {
            return await FindByCondition(b => b.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateDeskStatus(DeskStatus status)
        {
            Update(status);
        }
    }
}
