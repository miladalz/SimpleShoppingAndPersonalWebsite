using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteContactController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteContactController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetContactService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactDto contactDto)
        {
            return Json(_personalFacad.AddOrEditContactService.ExecuteAdd(contactDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetContactService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(ContactDto contactDto)
        {
            return Json(_personalFacad.AddOrEditContactService.ExecuteEdit(contactDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditContactService.ExecuteDelete(id));
        }
    }
}
