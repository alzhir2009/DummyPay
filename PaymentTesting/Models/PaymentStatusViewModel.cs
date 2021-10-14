using DummyPay.Models.Response;

namespace PaymentTesting.Models
{
    public class PaymentStatusViewModel
    {
        public PaymentStatusViewModel(StatusResult result)
        {
            TransactionId = result.TransactionId;
            Status = result.Status;
            Amount = result.Amount;
            Currency = result.Currency;
            OrderId = result.OrderId;
            LastFourDigits = result.LastFourDigits;
        }

        public string TransactionId { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public string LastFourDigits { get; set; }
    }
}
