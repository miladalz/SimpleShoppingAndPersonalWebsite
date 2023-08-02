using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModels.HomePages;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Products.Queries.GetProductForSite;
using EndPoint.Site.Models.ViewModels.Personal;
using MyWeb.Application.Services.Personals.Commands;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductFacad _productFacad;
        private readonly ICommonFacad _commonFacad;
        private readonly IPersonalFacad _personalFacad;
        public HomeController(ILogger<HomeController> logger, ICommonFacad commonFacad,
            IProductFacad productFacad, IPersonalFacad personalFacad)
        {
            _logger = logger;
            _commonFacad = commonFacad;
            _productFacad = productFacad;
            _personalFacad = personalFacad;
        }

        public IActionResult Store()
        {
            HomePageViewModel homePage = new HomePageViewModel()
            {
                Sliders = _commonFacad.GetSliderService.Execute().Data,
                PageImages = _commonFacad.GetHomePageImagesService.Execute().Data,
                Camera = _productFacad.GetProductForSiteService.Execute(Ordering.theNewest
                , null, 1, 6, 25).Data.Products,
            };
            return View(homePage);
        }

        public IActionResult Index()
        {
            var abouts = _personalFacad.GetAboutService.Execute().Data;
            var clients = _personalFacad.GetClientService.Execute().Data;
            var contacts = _personalFacad.GetContactService.Execute().Data;
            var homes = _personalFacad.GetHomeService.Execute().Data;
            var prices = _personalFacad.GetPriceService.Execute().Data;
            var projects = _personalFacad.GetProjectService.Execute().Data;
            var services = _personalFacad.GetServiceService.Execute().Data;
            var socialMedias = _personalFacad.GetSocialMediaService.Execute().Data;
            var supports = _personalFacad.GetSupportService.Execute().Data;
            var supportTop = supports.Where(a => a.Location == "top").FirstOrDefault();
            var supportMiddle = supports.Where(a => a.Location == "middle").FirstOrDefault();
            var supportBottom = supports.Where(a => a.Location == "bottom").FirstOrDefault();
            var supportFooter = supports.Where(a => a.Location == "footer").FirstOrDefault();
            var features = _personalFacad.GetFeatureService.Execute().Data;
            var counters = _personalFacad.GetCounterService.Execute().Data;
            var teams = _personalFacad.GetTeamService.Execute().Data;
            var personalViewModel = new PersonalViewModel
            {
                Abouts = abouts,
                Clients = clients,
                Contacts = contacts,
                Homes = homes,
                Prices = prices,
                Projects = projects,
                Services = services,
                SocialMedias = socialMedias,
                SupportTop = supportTop,
                SupportMiddle = supportMiddle,
                SupportBottom = supportBottom,
                SupportFooter = supportFooter,
                Features = features,
                Counters = counters,
                Teams = teams
            };
            return View(personalViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SendMessage(MessageDto messageDto)
        {
            var result = _personalFacad.AddOrEditMessageService.ExecuteAdd(messageDto);
            return Json(result);
        }

        [HttpPost]
        public IActionResult RegisterSiteNewsMember(SiteNewsMemberDto siteNewsMemberDto)
        {
            var result = _personalFacad.AddOrEditSiteNewsMemberService.ExecuteAdd(siteNewsMemberDto);
            return Json(result);
        }
    }
}
