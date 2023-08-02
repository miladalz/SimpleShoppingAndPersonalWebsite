using MyWeb.Domain.Entities.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class OrdersController : Controller
    {
        private readonly IOrderFacad _orderFacad;
        public OrdersController(IOrderFacad orderFacad)
        {
            _orderFacad = orderFacad;
        }
        public IActionResult Index(OrderState orderState, int Page = 1, int PageSize = 10)
        {
            return View(_orderFacad.GetOrdersForAdminService.Execute(orderState, Page, PageSize).Data);
        }

        [HttpPost]
        public IActionResult Delete(long Id)
        {
            return Json(_orderFacad.RemoveOrder.Execute(Id));
        }
    }
}
