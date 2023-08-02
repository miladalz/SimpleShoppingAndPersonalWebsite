using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteHomeController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteHomeController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetHomeService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeDto homeDto)
        {
            return Json(_personalFacad.AddOrEditHomeService.ExecuteAdd(homeDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetHomeService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(HomeDto homeDto)
        {
            return Json(_personalFacad.AddOrEditHomeService.ExecuteEdit(homeDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditHomeService.ExecuteDelete(id));
        }
    }
}
