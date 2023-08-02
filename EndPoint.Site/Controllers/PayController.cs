using Dto.Payment;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZarinPal.Class;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Orders.Command.AddNewOrder;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private readonly ICartsFacad _cartsFacad;
        private readonly CookiesManeger _cookiesManeger;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IOrderFacad _orderFacad;
        private readonly IFinancesFacad _fainancesFacad;
        public PayController(IFinancesFacad fainancesFacad, ICartsFacad cartsFacad, IOrderFacad orderFacad)
        {
            _fainancesFacad = fainancesFacad;
            _cartsFacad = cartsFacad;
            _cookiesManeger = new CookiesManeger();
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();            
            _orderFacad = orderFacad;
        }
        public async Task<IActionResult> Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartsFacad.CartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            if (cart.Data.SumAmount > 0)
            {
                var requestPay = _fainancesFacad.AddRequestPayService.Execute(cart.Data.SumAmount, UserId.Value);
                // ارسال در گاه پرداخت

                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = $"https://localhost:7007/Pay/Verify?guid={requestPay.Data.guid}",
                    Description = "پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId,
                    Email = requestPay.Data.Email,
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            var requestPay = _fainancesFacad.GetRequestPayService.Execute(guid);
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = requestPay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartsFacad.CartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            if (verification.Status == 100)
            {
                _orderFacad.AddNewOrderService.Execute(new RequestAddNewOrderServiceDto()
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    RequestPayId = requestPay.Data.Id,
                    Authority = authority,
                    RefId = 0
                });
                //redirect to orders
                return RedirectToAction("Index", "Orders");
            }
            else
            {
            }

            return View();
        }
    }
}
