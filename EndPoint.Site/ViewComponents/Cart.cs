using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    public class Cart:ViewComponent
    {
        private readonly ICartsFacad _cartsFacad;
        private readonly CookiesManeger _cookiesManeger;
        public Cart(ICartsFacad cartsFacad)
        {
            _cartsFacad = cartsFacad;
            _cookiesManeger = new CookiesManeger();
        }

        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            return View(viewName: "Cart", _cartsFacad.CartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext),userId).Data);
        }
    }
}
