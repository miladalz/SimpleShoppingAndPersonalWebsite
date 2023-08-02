using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteSupportController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteSupportController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetSupportService.ExecuteForAdmin(Page, PageSize).Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SupportDto supportDto)
        {
            return Json(_personalFacad.AddOrEditSupportService.ExecuteAdd(supportDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetSupportService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(SupportDto supportDto)
        {
            return Json(_personalFacad.AddOrEditSupportService.ExecuteEdit(supportDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditSupportService.ExecuteDelete(id));
        }
    }
}
