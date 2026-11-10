namespace Application.Payment.DTO
{
    public enum PaymentStatus
    {
        Sucess,
        Failed,
        Error,
        Undefined
    }
    public class PaymentStateDto
    {
        public PaymentStatus Status { get; set; }
        public string PaymentId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Message { get; set; }
    }
}
