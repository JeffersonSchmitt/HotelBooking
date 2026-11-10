namespace Application
{
    public abstract class Response
    {
        public enum ErrorCodes
        {
            //Guests error codes 1 99
            NOT_FOUND = 1,
            COULD_NOT_STORE_DATA = 2,
            INVALID_DOCUMENT = 3,
            INVALID_EMAIL = 4,
            MISSING_REQUIRED_INFORMATION = 5,


            //Rooms error codes 100 199
            INVALID_PRICE = 100,
            IS_NOT_AVAILABLE = 101,

            //payment error codes 200 299
            PAYMENT_INVALID = 200,

        }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
