using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartsFacad _cartsFacad;
        private readonly CookiesManeger cookiesManeger ;
        public CartController(ICartsFacad cartsFacad)
        {
            _cartsFacad = cartsFacad;
            cookiesManeger = new CookiesManeger();
        }

        public IActionResult Index()
        {
           var userId=  ClaimUtility.GetUserId(User);

           var resultGetLst= _cartsFacad.CartService.GetMyCart(cookiesManeger.GetBrowserId(HttpContext), userId);

            return View(resultGetLst.Data);
        }

        public IActionResult AddToCart(long ProductId)
        {          
           var resultAdd= _cartsFacad.CartService.AddToCart(ProductId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult Add(long CartItemId)
        {
            _cartsFacad.CartService.Add(CartItemId);
            return RedirectToAction("Index");
        }  
        
        public IActionResult LowOff(long CartItemId)
        {
            _cartsFacad.CartService.LowOff(CartItemId);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(long ProductId)
        {
            _cartsFacad.CartService.RemoveFromCart(ProductId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
    }
}
