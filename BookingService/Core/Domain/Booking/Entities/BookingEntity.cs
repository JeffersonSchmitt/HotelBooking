using Domain.Booking.Enums;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Entities;
using Domain.Room.Entities;
using Domain.Shared.Exceptions;
using Action = Domain.Booking.Enums.Action;

namespace Domain.Entities
{
    public class BookingEntity
    {
        public BookingEntity()
        {
            this.Status = Status.Created;
            this.PlacedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RoomEntity Room { get; set; }
        public GuestEntity Guest { get; set; }
        public Status Status { get; set; }

        public void ChangeState(Action action)
        {
            this.Status = (this.Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Cancelled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refund) => Status.Refunded,
                (Status.Cancelled, Action.Reopen) => Status.Created,
                _ => this.Status,
            };
        }
        private void ValidateState()
        {
            if (this.PlacedAt == default || this.Start == default || this.End == default || this.Guest == null || this.Room == null)
            {
                throw new MissingRequiredInformationException();
            }
        }
        public bool IsValid()
        {
            try
            {
                ValidateState();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task Save(IBookingRepository bookingRepository)
        {
            ValidateState();
            if (!this.Room.CanBeBooked())
            {
                throw new RoomCannotBeBookedException();
            }

            if (Id == 0)
            {
                Id = await bookingRepository.CreateBooking(this);
            }
            else
            {
                // await guestRepository.Update(this);
            }
        }
    }
}
