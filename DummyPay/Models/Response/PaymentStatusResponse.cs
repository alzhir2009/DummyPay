using System;
using System.Collections.Generic;
using System.Text;
using DummyPay.Interfaces.Models;

namespace DummyPay.Models.Response
{
    public class PaymentStatusResponse : ApiResponse
    {
        public StatusResult Result { get; set; }
    }

    public class StatusResult
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public string LastFourDigits { get; set; }
    }

}
