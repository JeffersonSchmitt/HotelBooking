using Domain.Guest.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private HotelDbContext _hotelDbContext;
        public GuestRepository( HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Domain.Guest.Entities.GuestEntity guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public Task<Domain.Guest.Entities.GuestEntity> Get(int id)
        {
            return _hotelDbContext.Guests.Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public Task<Domain.Guest.Entities.GuestEntity> GetByName(string name)
        {
            return _hotelDbContext.Guests.Where(g => g.Name == name).FirstOrDefaultAsync();
        }
    }
}
