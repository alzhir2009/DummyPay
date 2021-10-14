namespace DummyPay.Models
{
    public enum PaymentStatus
    {
        Init,
        Pending,
        Approved,
        Declined,
        DeclinedDueToInvalidCreditCard
    }
}
