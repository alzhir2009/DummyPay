using DummyPay.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DummyPay.Constants;
using DummyPay.Models.Request;
using DummyPay.Models.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DummyPay.Services
{
    public class RequestService : IRequestService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RequestService> _logger;

        public RequestService(IHttpClientFactory httpClientFactory, ILogger<RequestService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public Task<CreatePaymentResponse> MakeCreatePaymentRequestAsync(CreatePaymentRequest request)
        {
            return PostAsync<CreatePaymentResponse>("create", request);
        }

        public Task<ConfirmPaymentResponse> MakeConfirmPaymentRequestAsync(ConfirmPaymentRequest request)
        {
            return PostAsync<ConfirmPaymentResponse>("confirm", request);
        }

        public Task<PaymentStatusResponse> MakePaymentStatusRequestAsync(string transactionId)
        {
            return GetAsync<PaymentStatusResponse>($"{transactionId}/status");
        }

        private async Task<TOut> PostAsync<TOut>(string url, object requestObject)
        {
            var responseString = String.Empty;

            try
            {
                var requestString = JsonConvert.SerializeObject(requestObject);

                var httpClient = _httpClientFactory.CreateClient(InnerConstants.HttpClientName);
                var response =
                    await httpClient.PostAsync(url, new StringContent(requestString, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TOut>(responseString);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error during POST request: {URL}, request: {@RequestObject}", url, requestObject);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JsonConvert exception request: {@RequestObject} response: {@ResponseString}", requestObject, responseString);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                throw;
            }
        }

        private async Task<TOut> GetAsync<TOut>(string url)
        {
            var responseString = String.Empty;

            try
            {
                var httpClient = _httpClientFactory.CreateClient(InnerConstants.HttpClientName);
                responseString = await httpClient.GetStringAsync(url);

                return JsonConvert.DeserializeObject<TOut>(responseString);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error during GET request: {URL}", url);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JsonConvert exception: {ResponseString}", responseString);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                throw;
            }
        }
    }
}
