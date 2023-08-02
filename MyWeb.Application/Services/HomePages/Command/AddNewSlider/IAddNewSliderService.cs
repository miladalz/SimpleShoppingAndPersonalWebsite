using MyWeb.Common.Dto;
using Microsoft.AspNetCore.Http;

namespace MyWeb.Application.Services.HomePages.Command.AddNewSlider
{
    public interface IAddNewSliderService
    {
        ResultDto Execute(IFormFile file, string Link);
    }
}
