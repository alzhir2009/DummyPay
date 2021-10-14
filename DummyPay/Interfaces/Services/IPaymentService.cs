using System.Threading.Tasks;
using DummyPay.Models.Request;
using DummyPay.Models.Response;

namespace DummyPay.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request);

        Task<ConfirmPaymentResponse> ConfirmPaymentAsync(ConfirmPaymentRequest request);

        Task<PaymentStatusResponse> GetStatusAsync(string transactionId);
    }
}
