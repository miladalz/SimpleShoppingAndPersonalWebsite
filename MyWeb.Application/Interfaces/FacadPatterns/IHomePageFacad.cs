using MyWeb.Application.Services.HomePages.Command.AddHomePageImages;
using MyWeb.Application.Services.HomePages.Command.AddNewSlider;
using MyWeb.Application.Services.HomePages.Command.RemoveHomePageImage;
using MyWeb.Application.Services.HomePages.Command.RemoveSlider;
using MyWeb.Application.Services.HomePages.Queries.GetHomePagesForAdmin;
using MyWeb.Application.Services.HomePages.Queries.GetSildersForAdmin;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface IHomePageFacad
    {
        IAddHomePageImagesService AddHomePageImagesService { get; }
        IAddNewSliderService AddNewSliderService { get; }
        IGetSildersForAdmin GetSildersForAdmin { get; }
        IGetHomePagesForAdmin GetHomePagesForAdmin { get; }
        IRemoveHomePageImage RemoveHomePageImage { get; }
        IRemoveSlider RemoveSlider { get; }
    }
}
