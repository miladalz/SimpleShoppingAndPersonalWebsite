using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Common.Queries.GetCategory;
using MyWeb.Application.Services.Common.Queries.GetHomePageImages;
using MyWeb.Application.Services.Common.Queries.GetMenuItem;
using MyWeb.Application.Services.Common.Queries.GetSlider;

namespace MyWeb.Application.Services.Common.FacadPattern
{
    public class CommonFacad : ICommonFacad
    {
        private readonly IDataBaseContext _context;
        public CommonFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IGetCategoryService _getCategoryService;
        public IGetCategoryService GetCategoryService
        {
            get
            {
                return _getCategoryService ??= new GetCategoryService(_context);
            }
        }

        private IGetHomePageImagesService _getHomePageImagesService;
        public IGetHomePageImagesService GetHomePageImagesService
        {
            get
            {
                return _getHomePageImagesService ??= new GetHomePageImagesService(_context);
            }
        }

        private IGetMenuItemService _getMenuItemService;
        public IGetMenuItemService GetMenuItemService
        {
            get
            {
                return _getMenuItemService ??= new GetMenuItemService(_context);
            }
        }

        private IGetSliderService _getSliderService;
        public IGetSliderService GetSliderService
        {
            get
            {
                return _getSliderService ??= new GetSliderService(_context);
            }
        }
    }
}
