using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteFeatureController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteFeatureController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetFeatureService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FeatureDto featureDto)
        {
            return Json(_personalFacad.AddOrEditFeatureService.ExecuteAdd(featureDto));
        }

        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetFeatureService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(FeatureDto featureDto)
        {
            return Json(_personalFacad.AddOrEditFeatureService.ExecuteEdit(featureDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditFeatureService.ExecuteDelete(id));
        }
    }
}
