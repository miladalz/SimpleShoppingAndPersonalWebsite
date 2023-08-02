using MyWeb.Domain.Entities.HomePages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.HomePages.Command.AddHomePageImages;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class HomePageImagesController : Controller
    {
        private readonly IHomePageFacad _homePageFacad;
        public HomePageImagesController(IHomePageFacad homePageFacad)
        {
            _homePageFacad = homePageFacad;
        }
        public IActionResult Index(int Page = 1, int PageSize = 10)
        {
            return View(_homePageFacad.GetHomePagesForAdmin.Execute(Page, PageSize).Data);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link , ImageLocation imageLocation)
        {
            _homePageFacad.AddHomePageImagesService.Execute(new RequestAddHomePageImagesDto
            {
                file = file,
                ImageLocation = imageLocation,
                Link = link,
            });
            return View();
        }

        [HttpPost]
        public IActionResult Delete(long Id)
        {
            return Json(_homePageFacad.RemoveHomePageImage.Execute(Id));
        }
    }
}
