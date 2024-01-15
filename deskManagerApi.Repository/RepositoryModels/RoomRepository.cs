using deskManagerApi.Contracts.IRepositoryModels;
using deskManagerApi.Entities;
using deskManagerApi.IRepository;
using deskManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Repository.RepositoryModels
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateRoom(Room room)
        {
            await Create(room);
        }

        public void DeleteRoom(Room room)
        {
            Delete(room);
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await FindAll().OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAllRoomsByFloorId(int id)
        {
            return await FindAll().Where(r => r.FloorId == id).OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await FindByCondition(r => r.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateRoom(Room room)
        {
            Update(room);
        }
    }
}
