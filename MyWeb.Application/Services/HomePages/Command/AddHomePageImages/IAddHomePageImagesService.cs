using Microsoft.AspNetCore.Http;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.HomePages;

namespace MyWeb.Application.Services.HomePages.Command.AddHomePageImages
{
    public interface IAddHomePageImagesService
    {
        ResultDto Execute(RequestAddHomePageImagesDto request);
    }

    public class RequestAddHomePageImagesDto
    {
        public IFormFile file { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
