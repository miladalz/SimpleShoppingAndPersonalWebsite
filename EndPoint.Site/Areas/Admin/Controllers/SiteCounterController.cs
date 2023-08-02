using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteCounterController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteCounterController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetCounterService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CounterDto counterDto)
        {
            return Json(_personalFacad.AddOrEditCounterService.ExecuteAdd(counterDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetCounterService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(CounterDto counterDto)
        {
            return Json(_personalFacad.AddOrEditCounterService.ExecuteEdit(counterDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditCounterService.ExecuteDelete(id));
        }
    }
}
