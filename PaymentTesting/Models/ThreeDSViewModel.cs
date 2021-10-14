using DummyPay.Models.Response;

namespace PaymentTesting.Models
{
    public class ThreeDSViewModel
    {
        public ThreeDSViewModel(CreateResult result)
        {
            Method = result.Method;
            Url = result.Url;
            PaReq = result.PaReq;
            TransactionId = result.TransactionId;
        }

        public string TransactionId { get; }

        public string Method { get; }

        public string Url { get; }

        public string PaReq { get; }
    }
}
