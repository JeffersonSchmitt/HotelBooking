using Domain.Room.Enums;

namespace Domain.Shared.ValueObjects
{
    public class Price
    {
        public AcceptedCurrencies Currency { get; set; }
        public decimal Value { get; set; }

    }
}
