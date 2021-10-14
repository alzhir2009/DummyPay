namespace DummyPay.Models.Request
{
    public class ConfirmPaymentRequest
    {
        public string TransactionId { get; set; }
        public string PaRes { get; set; }
    }

}
