using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.HomePages;

namespace MyWeb.Application.Services.HomePages.Queries.GetHomePagesForAdmin
{
    public interface IGetHomePagesForAdmin
    {
        ResultDto<HomePageImagePage> Execute(int Page = 1, int PageSize = 10);
    }

    public class HomePageImagePage : ResultPage
    {
        public List<HomePageImage> HomePageImages { get; set; }
    }

    public class HomePageImage
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
