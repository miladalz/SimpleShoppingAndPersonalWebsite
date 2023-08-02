using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SitePriceController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SitePriceController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetPriceService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PriceDto priceDto)
        {
            return Json(_personalFacad.AddOrEditPriceService.ExecuteAdd(priceDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetPriceService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(PriceDto priceDto)
        {
            return Json(_personalFacad.AddOrEditPriceService.ExecuteEdit(priceDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditPriceService.ExecuteDelete(id));
        }
    }
}
