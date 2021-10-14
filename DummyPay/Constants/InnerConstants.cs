using System;
using System.Collections.Generic;
using DummyPay.Models;

namespace DummyPay.Constants
{
    public static class InnerConstants
    {
        public const string PaymentApiUrl = "https://dumdumpay.site/api/payment/";

        public const string HttpClientName = "DummyPayHttpClient";

        public static readonly Dictionary<PaymentStatus, string> TestCardDictionary = new Dictionary<PaymentStatus, string>
        {
            {PaymentStatus.Approved, "4111111111111111"},
            {PaymentStatus.Declined, "4111111111111112"},
            {PaymentStatus.DeclinedDueToInvalidCreditCard, "4007702835532454"}
        };
    }
}
