using System;
using System.Collections.Generic;
using DummyPay.Interfaces.Models;

namespace DummyPay.Models.Response
{
    public class CreatePaymentResponse : ApiResponse
    {
        public CreateResult Result { get; set; }
    }

    public class CreateResult
    {
        public string TransactionId { get; set; }
        public string TransactionStatus { get; set; }
        public string PaReq { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
    }
}