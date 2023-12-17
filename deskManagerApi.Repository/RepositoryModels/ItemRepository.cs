using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Repository.RepositoryModels
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateItem(Item item)
        {
            await Create(item);
        }

        public void DeleteItem(Item item)
        {
            Delete(item);
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await FindAll().OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<Item> GetItemById(int id)
        {
            return await FindByCondition(i => i.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateItem(Item item)
        {
            Update(item);
        }
    }
}
