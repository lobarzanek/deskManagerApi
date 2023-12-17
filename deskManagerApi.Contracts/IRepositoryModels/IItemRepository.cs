using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IItemRepository : IRepositoryBase<Item>
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetItemById(int id);
        Task CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}
