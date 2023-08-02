using Microsoft.AspNetCore.Mvc;
using MyWeb.Application.Interfaces.FacadPatterns;
namespace EndPoint.Site.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly ICommonFacad _commonFacad;
        public GetMenu(ICommonFacad commonFacad)
        {
            _commonFacad = commonFacad;
        }
        public IViewComponentResult Invoke()
        {
            var menuItem = _commonFacad.GetMenuItemService.Execute();
            return View(viewName: "GetMenu", menuItem.Data);
        }
    }
}
