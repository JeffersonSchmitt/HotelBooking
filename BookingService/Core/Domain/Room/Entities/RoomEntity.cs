using Domain.Entities;
using Domain.Room.Exceptions;
using Domain.Room.Ports;
using Domain.Shared.Exceptions;
using Domain.Shared.ValueObjects;

namespace Domain.Room.Entities
{
    public class RoomEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }

        public ICollection<BookingEntity> Bookings { get; set; }

        public bool IsAvailable
        {
            get
            {
                if (this.InMaintenance || this.hasGuest)
                {
                    return false;
                }
                return true;
            }
        }
        public bool hasGuest
        {
            get
            {
                var notAvailableStatuses = new List<Booking.Enums.Status>()
                {
                    Booking.Enums.Status.Created,
                    Booking.Enums.Status.Paid,
                };

                return this.Bookings?.Where(
                    b => b.Room.Id == this.Id &&
                    notAvailableStatuses.Contains(b.Status)).Count() > 0;
            }
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name) || level < 0)
            {
                throw new MissingRequiredInformationException();
            }
            if (Price == null || Price.Value < 0 || Price.Currency < 0)
            {
                throw new InvalidPriceException();
            }

        }

        public bool CanBeBooked()
        {
            try
            {
                this.ValidateState();
            }
            catch (Exception)
            {

                return false;
            }

            if (!this.IsAvailable)
            {
                return false;
            }

            return true;

        }

        public async Task Save(IRoomRepository roomRepository)
        {
            ValidateState();
            if (Id == 0)
            {
                Id = await roomRepository.CreateRoom(this);
            }
            else
            {
                // await roomRepository.Update(this);
            }
        }
    }
}
