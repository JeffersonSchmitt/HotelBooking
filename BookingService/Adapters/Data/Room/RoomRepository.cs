using Domain.Room.Entities;
using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private HotelDbContext _hotelDbContext;
        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;

        }

        public async Task<int> CreateRoom(RoomEntity room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public Task<RoomEntity> GetRoom(int id)
        {
            return _hotelDbContext.Rooms.Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public Task<RoomEntity> GetRoomByName(string name)
        {
            return _hotelDbContext.Rooms.Where(g => g.Name == name).FirstOrDefaultAsync();
        }
    }
}
