using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.HomePages.Queries.GetSildersForAdmin
{
    public interface IGetSildersForAdmin
    {
        ResultDto<SilderPage> Execute(int Page = 1, int PageSize = 10);
    }

    public class SilderPage : ResultPage
    {
        public List<Slider> Sliders { get; set; }
    }

    public class Slider
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
