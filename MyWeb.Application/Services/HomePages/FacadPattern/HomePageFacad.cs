using Microsoft.AspNetCore.Hosting;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.HomePages.Command.AddHomePageImages;
using MyWeb.Application.Services.HomePages.Command.AddNewSlider;
using MyWeb.Application.Services.HomePages.Command.RemoveHomePageImage;
using MyWeb.Application.Services.HomePages.Command.RemoveSlider;
using MyWeb.Application.Services.HomePages.Queries.GetHomePagesForAdmin;
using MyWeb.Application.Services.HomePages.Queries.GetSildersForAdmin;

namespace MyWeb.Application.Services.HomePages.FacadPattern
{
    public class HomePageFacad : IHomePageFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public HomePageFacad(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private IAddHomePageImagesService _addHomePageImagesService;
        public IAddHomePageImagesService AddHomePageImagesService
        {
            get
            {
                return _addHomePageImagesService ??= new AddHomePageImagesService(_context, _environment);
            }
        }

        private IAddNewSliderService _addNewSliderService;
        public IAddNewSliderService AddNewSliderService
        {
            get
            {
                return _addNewSliderService ??= new AddNewSliderService(_context, _environment);
            }
        }

        private IGetSildersForAdmin _getSildersForAdmin;
        public IGetSildersForAdmin GetSildersForAdmin
        {
            get
            {
                return _getSildersForAdmin ??= new GetSildersForAdmin(_context);
            }
        }

        private IGetHomePagesForAdmin _getHomePagesForAdmin;
        public IGetHomePagesForAdmin GetHomePagesForAdmin
        {
            get
            {
                return _getHomePagesForAdmin ??= new GetHomePagesForAdmin(_context);
            }
        }


        private IRemoveHomePageImage _removeHomePageImage;
        public IRemoveHomePageImage RemoveHomePageImage
        {
            get
            {
                return _removeHomePageImage ??= new RemoveHomePageImage(_context);
            }
        }

        private IRemoveSlider _removeSlider;
        public IRemoveSlider RemoveSlider
        {
            get
            {
                return _removeSlider ??= new RemoveSlider(_context);
            }
        }
    }
}
