using System;
using System.Collections.Generic;
using System.Text;

namespace DummyPay.Models
{
    public class DummyPayConfiguration
    {
        public const string SectionName = "DummyPay";

        public string MerchantId { get; set; }

        public string SecretKey { get; set; }
    }
}
