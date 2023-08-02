using MyWeb.Application.Services.Common.Queries.GetCategory;
using MyWeb.Application.Services.Common.Queries.GetHomePageImages;
using MyWeb.Application.Services.Common.Queries.GetMenuItem;
using MyWeb.Application.Services.Common.Queries.GetSlider;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface ICommonFacad
    {
        IGetCategoryService GetCategoryService { get; }
        IGetHomePageImagesService GetHomePageImagesService { get; }
        IGetMenuItemService GetMenuItemService { get; }
        IGetSliderService GetSliderService { get; }
    }
}
