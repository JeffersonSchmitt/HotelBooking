namespace Application
{
    public abstract class Response
    {
        public enum ErrorCodes
        {
            NOT_FOUND = 1,
            COULD_NOT_STORE_DATA = 2,
            INVALID_DOCUMENT = 3,
            INVALID_EMAIL = 4,
            MISSING_REQUIRED_INFORMATION = 5
        }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
