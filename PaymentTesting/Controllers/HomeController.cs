using Microsoft.AspNetCore.Mvc;
using PaymentTesting.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using DummyPay.Constants;
using DummyPay.Interfaces.Services;
using DummyPay.Models;
using DummyPay.Models.Request;
using DummyPay.Models.Response;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace PaymentTesting.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IMemoryCache _cache;

        public HomeController(IPaymentService paymentService, IMemoryCache cache)
        {
            _paymentService = paymentService;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("TestApproved")]
        public async Task<IActionResult> TestApproved() => await Test(PaymentStatus.Approved);

        [HttpGet("TestDeclined")]
        public async Task<IActionResult> TestDeclined() => await Test(PaymentStatus.DeclinedDueToInvalidCreditCard);

        [HttpGet("ThreeDSResponseUrl")]
        public async Task<IActionResult> ThreeDSResponseUrl(string md, string paRes)
        {
            var confirmRequest = new ConfirmPaymentRequest()
            {
                PaRes = paRes,
                TransactionId = md
            };

            var response = await _paymentService.ConfirmPaymentAsync(confirmRequest);

            if (response.Result != default)
            {
                var statusResponse = await _paymentService.GetStatusAsync(response.Result.TransactionId);

                _cache.Set(response.Result.TransactionId, statusResponse.Result);

                return RedirectToAction("PaymentStatusPage", "Home", new { transactionId = statusResponse.Result.TransactionId });
            }

            return RedirectToAction("Error", new { errors = string.Join(".", response.Errors.Select(x => x.Message)) });
        }

        private async Task<IActionResult> Test(PaymentStatus status)
        {
            var testRequest = new CreatePaymentRequest()
            {
                OrderId = "DBB99946-A10A-4D1B-A742-577FA026BC57",
                Amount = 12312,
                Country = "CY",
                Currency = "USD",
                CardNumber = InnerConstants.TestCardDictionary[status],
                CardHolder = "TEST TESTER",
                CardExpiryDate = "1123",
                CVV = "111"
            };

            var response = await _paymentService.CreatePaymentAsync(testRequest);

            return PageFor3DS(response);
        }

        private IActionResult PageFor3DS(CreatePaymentResponse response)
        {
            if (response.Result != default)
            {
                return View("PageFor3DS", new ThreeDSViewModel(response.Result));
            }

            return RedirectToAction("Error", new { errors = string.Join(".", response.Errors.Select(x => x.Message)) });
        }


        public IActionResult PaymentStatusPage(string transactionId)
        {
            var result = _cache.Get<StatusResult>(transactionId);

            return View(new PaymentStatusViewModel(result));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errors)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = errors });
        }
    }
}
