using Domain.Booking.Enums;
using Action = Domain.Booking.Enums.Action;
using Domain.Guest.Entities;
using Domain.Room.Entities;

namespace Domain.Entities
{
    public class BookingEntity
    {
        public BookingEntity()
        {
            this.Status = Status.Created;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RoomEntity Room { get; set; }
        public GuestEntity Guest { get; set; }
        private Status Status { get; set; }

        public Status CurrentStatus
        {
            get { return this.Status; }
        }

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
    }
}
