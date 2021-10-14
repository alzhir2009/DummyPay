using DummyPay.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DummyPay.Interfaces.Models;
using DummyPay.Models.Request;
using DummyPay.Models.Response;
using Microsoft.Extensions.Logging;

namespace DummyPay.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IRequestService requestService, ILogger<PaymentService> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        public Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
        {
            return WithErrorLogs(async () =>
            {
                var result = await _requestService.MakeCreatePaymentRequestAsync(request);

                if (result.Result != default)
                {
                    result.Result.Url = result.Result.Url.Replace("dummypay.host", "dumdumpay.site");
                }

                return result;
            });
        }

        public Task<ConfirmPaymentResponse> ConfirmPaymentAsync(ConfirmPaymentRequest request)
        {
            return WithErrorLogs(() => _requestService.MakeConfirmPaymentRequestAsync(request));
        }

        public Task<PaymentStatusResponse> GetStatusAsync(string transactionId)
        {
            return WithErrorLogs(() => _requestService.MakePaymentStatusRequestAsync(transactionId));
        }

        private async Task<TOut> WithErrorLogs<TOut>(Func<Task<TOut>> requestFunc) where TOut : ApiResponse
        {
            var response = await requestFunc.Invoke();

            if (response.Errors?.Count > 0)
            {
                LogApiErrors(response.Errors);
            }

            return response;
        }

        private void LogApiErrors(List<ResponseError> errors)
        {
            _logger.LogError("Errors during API call. Errors: {@Errors}", errors);
        }
    }
}
