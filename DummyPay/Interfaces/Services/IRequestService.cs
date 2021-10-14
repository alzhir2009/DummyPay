using System.Threading.Tasks;
using DummyPay.Models.Request;
using DummyPay.Models.Response;

namespace DummyPay.Interfaces.Services
{
    public interface IRequestService
    {
        Task<CreatePaymentResponse> MakeCreatePaymentRequestAsync(CreatePaymentRequest request);

        Task<ConfirmPaymentResponse> MakeConfirmPaymentRequestAsync(ConfirmPaymentRequest request);

        Task<PaymentStatusResponse> MakePaymentStatusRequestAsync(string transactionId);
    }
}
