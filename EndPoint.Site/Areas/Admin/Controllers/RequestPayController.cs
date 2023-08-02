using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class RequestPayController : Controller
    {
        private readonly IFinancesFacad _fainancesFacad;
        public RequestPayController(IFinancesFacad fainancesFacad)
        {
            _fainancesFacad = fainancesFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_fainancesFacad.GetRequestPayForAdminService.Execute(Page, PageSize).Data);
        }

        [HttpPost]
        public IActionResult Delete(long Id)
        {
            return Json(_fainancesFacad.RemovePay.Execute(Id));
        }
    }
}
