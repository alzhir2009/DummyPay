using System;
using System.Collections.Generic;
using DummyPay.Constants;
using DummyPay.Interfaces.Services;
using DummyPay.Models;
using DummyPay.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DummyPay.Extensions
{
    public static class DependencyInjectionsExtensions
    {
        public static void AddDummyPayHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var dummyPayConfig = configuration.GetSection(DummyPayConfiguration.SectionName).Get<DummyPayConfiguration>();

            if (String.IsNullOrEmpty(dummyPayConfig.MerchantId) || String.IsNullOrEmpty(dummyPayConfig.SecretKey))
                throw new KeyNotFoundException($"{DummyPayConfiguration.SectionName} is not found in appsettings.json");

            services.AddHttpClient(InnerConstants.HttpClientName, config =>
            {
                config.BaseAddress = new Uri(InnerConstants.PaymentApiUrl);
                config.Timeout = new TimeSpan(0, 0, 30);
                config.DefaultRequestHeaders.Add("Mechant-Id", dummyPayConfig.MerchantId);
                config.DefaultRequestHeaders.Add("Secret-Key", dummyPayConfig.SecretKey);
            });
        }

        public static void AddDummyPayServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestService, RequestService>();
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}
