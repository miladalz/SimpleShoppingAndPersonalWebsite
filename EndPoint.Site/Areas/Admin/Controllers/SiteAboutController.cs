using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteAboutController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteAboutController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetAboutService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AboutDto aboutDto)
        {
            return Json(_personalFacad.AddOrEditAboutService.ExecuteAdd(aboutDto));
        }

        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetAboutService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(AboutDto aboutDto)
        {
            return Json(_personalFacad.AddOrEditAboutService.ExecuteEdit(aboutDto));
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditAboutService.ExecuteDelete(id));
        }
    }
}
