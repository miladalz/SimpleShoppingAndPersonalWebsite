using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SiteClientController : Controller
    {
        private readonly IPersonalFacad _personalFacad;
        public SiteClientController(IPersonalFacad personalFacad)
        {
            _personalFacad = personalFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_personalFacad.GetClientService.ExecuteForAdmin(Page, PageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClientDto clientDto)
        {
            return Json(_personalFacad.AddOrEditClientService.ExecuteAdd(clientDto));
        }
        [HttpPost]
        public IActionResult Edit(long id)
        {
            return PartialView("_PartialEdit", _personalFacad.GetClientService.ExecuteForEdit(id));
        }
        [HttpPost]
        public IActionResult SubmitEdit(ClientDto clientDto)
        {
            return Json(_personalFacad.AddOrEditClientService.ExecuteEdit(clientDto));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            return Json(_personalFacad.AddOrEditClientService.ExecuteDelete(id));
        }
    }
}
