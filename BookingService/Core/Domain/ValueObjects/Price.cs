using Domain.Enums;

namespace Domain.ValueObjects
{
    public class Price
    {
        public AcceptedCurrencies Currency { get; set; }
        public decimal Value { get; set; }

    }
}
