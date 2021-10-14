using System.Collections.Generic;
using DummyPay.Models.Response;

namespace DummyPay.Interfaces.Models
{
    public abstract class ApiResponse
    {
        public List<ResponseError> Errors { get; set; }
    }
}
