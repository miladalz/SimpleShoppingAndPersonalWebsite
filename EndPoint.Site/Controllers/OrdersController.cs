using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderFacad _orderFacad;
        public OrdersController(IOrderFacad orderFacad)
        {
            _orderFacad = orderFacad;
        }

        public IActionResult Index()
        {
            long userId = ClaimUtility.GetUserId(User).Value;
            return View(_orderFacad.GetUserOrdersService.Execute(userId).Data);
        }
    }
}
