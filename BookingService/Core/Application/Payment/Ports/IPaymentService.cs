using Application.Payment.DTO;
using Application.Payment.Responses;

namespace Application.Payment.Ports
{
    public interface IPaymentService
    {
        Task<PaymentResponse> PayWithCreditCard(string paymentIntention);
    }
}
