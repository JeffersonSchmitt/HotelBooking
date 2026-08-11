using Domain.Booking.Enums;
using Domain.Entities;
using Action = Domain.Booking.Enums.Action;

namespace DomainTests.Bookings
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAwaysStatWithCreatedStatus()
        {
            var booking = new BookingEntity();
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new BookingEntity();
            booking.ChangeState(Action.Pay);
            Assert.AreEqual(booking.CurrentStatus, Status.Paid);
        }

        [Test]
        public void ShouldSetStatusToCancelledWhenCancellingBookingWithCreatedStatus()
        {
            var booking = new BookingEntity();
            booking.ChangeState(Action.Cancel);
            Assert.AreEqual(booking.CurrentStatus, Status.Cancelled);
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingBookingWithPaidStatus()
        {
            var booking = new BookingEntity();
            booking.ChangeState(Action.Pay); // deixa como Paid
            booking.ChangeState(Action.Finish);
            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }

        [Test]
        public void ShouldSetStatusToRefundedWhenRefundingBookingWithPaidStatus()
        {
            var booking = new BookingEntity();
            booking.ChangeState(Action.Pay); // deixa como Paid
            booking.ChangeState(Action.Refund);
            Assert.AreEqual(booking.CurrentStatus, Status.Refunded);
        }

        [Test]
        public void ShouldReopenCancelledBookingToCreatedStatus()
        {
            var booking = new BookingEntity();
            booking.ChangeState(Action.Cancel);
            Assert.AreEqual(booking.CurrentStatus, Status.Cancelled);
            booking.ChangeState(Action.Reopen);
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusForInvalidAction()
        {
            var booking = new BookingEntity();
            // Ação inválida para o estado Created — não deve alterar o estado
            booking.ChangeState(Action.Finish);
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }
    }
}