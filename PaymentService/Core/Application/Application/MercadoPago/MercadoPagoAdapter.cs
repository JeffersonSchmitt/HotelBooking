using Application.MercadoPago.Exceptions;
using Application.Payment;
using Application.Payment.DTO;
using Application.Payment.Ports;
using Application.Payment.Responses;
using static Application.Response;

namespace Payment.Application.MercadoPago
{
    public class MercadoPagoAdapter : IPaymentService
    {
        public Task<PaymentResponse> PayWithCreditCard(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paymentIntention))
                {
                    throw new InvalidPaymentException();
                }

                var dto = new PaymentStateDto
                {
                    Status = PaymentStatus.Sucess,
                    PaymentId = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow,
                    Message = $"Payment successful {paymentIntention}"
                };
                var response = new PaymentResponse
                {
                    Data = dto,
                    Success = true,

                };

                return Task.FromResult(response);
            }
            catch (InvalidPaymentException)
            {
                var resp= new PaymentResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PAYMENT_INVALID

                };
                return Task.FromResult(resp);
            }
        }
    }
}
