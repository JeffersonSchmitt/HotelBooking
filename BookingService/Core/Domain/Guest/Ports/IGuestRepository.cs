using Domain.Guest.Entities;

namespace Domain.Guest.Ports
{
    public interface IGuestRepository
    {
        Task<GuestEntity> Get(int id);
        Task<GuestEntity> GetByName(string name);
        Task<int> Create(GuestEntity guest);
    }
}
