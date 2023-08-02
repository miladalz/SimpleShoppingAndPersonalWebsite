using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteServiceController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteServiceController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetServiceService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceDto serviceDto)
        {
            return Json(_personalFacad.AddOrEditServiceService.ExecuteAdd(serviceDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetServiceService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(ServiceDto serviceDto)
        {
            return Json(_personalFacad.AddOrEditServiceService.ExecuteEdit(serviceDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditServiceService.ExecuteDelete(id));
        }
    }
}
