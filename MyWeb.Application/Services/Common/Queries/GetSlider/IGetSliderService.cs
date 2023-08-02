using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Common.Queries.GetSlider
{
    public interface IGetSliderService
    {
        ResultDto<List<SliderDto>> Execute();
    }

    public class SliderDto
    {
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
