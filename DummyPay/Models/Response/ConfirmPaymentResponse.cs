using DummyPay.Interfaces.Models;

namespace DummyPay.Models.Response
{
    public class ConfirmPaymentResponse : ApiResponse
    {
        public ConfirmResult Result { get; set; }
    }

    public class ConfirmResult
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public string LastFourDigits { get; set; }
    }

}
