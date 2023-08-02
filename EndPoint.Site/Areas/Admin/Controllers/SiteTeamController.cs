using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteTeamController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteTeamController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetTeamService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TeamDto teamDto)
        {
            return Json(_personalFacad.AddOrEditTeamService.ExecuteAdd(teamDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetTeamService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(TeamDto teamDto)
        {
            return Json(_personalFacad.AddOrEditTeamService.ExecuteEdit(teamDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditTeamService.ExecuteDelete(id));
        }
    }
}
