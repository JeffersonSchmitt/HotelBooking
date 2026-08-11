using Domain.Room.Entities;

namespace Domain.Room.Ports
{
    public interface IRoomRepository
    {
        Task<RoomEntity> GetRoom(int id);
        Task<RoomEntity> GetRoomByName(string room);

        Task<int> CreateRoom(RoomEntity room);

    }
}
