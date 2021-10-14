using System;
using System.Collections.Generic;
using System.Text;

namespace DummyPay.Models.Request
{
    public class CreatePaymentRequest
    {
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string CardExpiryDate { get; set; }
        public string CVV { get; set; }
    }
}
