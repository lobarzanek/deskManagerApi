using deskManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deskManagerApi.Contracts.IRepositoryModels
{
    public interface IRoomRepository : IRepositoryBase<Room>
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<IEnumerable<Room>> GetAllRoomsByFloorId(int id);
        Task<Room> GetRoomById(int id);
        Task CreateRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
    }
}
