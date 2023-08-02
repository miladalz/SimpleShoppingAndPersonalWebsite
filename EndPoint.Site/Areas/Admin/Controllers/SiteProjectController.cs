using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteProjectController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteProjectController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetProjectService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProjectDto projectDto)
        {
            return Json(_personalFacad.AddOrEditProjectService.ExecuteAdd(projectDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetProjectService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(ProjectDto projectDto)
        {
            return Json(_personalFacad.AddOrEditProjectService.ExecuteEdit(projectDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditProjectService.ExecuteDelete(id));
        }
    }
}
