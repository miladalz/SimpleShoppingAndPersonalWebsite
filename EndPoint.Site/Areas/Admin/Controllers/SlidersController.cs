using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SlidersController : Controller
    {
        private readonly IHomePageFacad _homePageFacad;
        public SlidersController(IHomePageFacad homePageFacad)
        {
            _homePageFacad = homePageFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_homePageFacad.GetSildersForAdmin.Execute(Page, PageSize).Data);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link)
        {
            _homePageFacad.AddNewSliderService.Execute(file, link);
            return View();
        }

        [HttpPost]
        public IActionResult Delete(long Id)
        {
            return Json(_homePageFacad.RemoveSlider.Execute(Id));
        }
    }
}
