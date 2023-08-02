using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.HomePages.Command.RemoveSlider
{
    public interface IRemoveSlider
    {
        ResultDto Execute(long Id);
    }

    public class RemoveSlider : IRemoveSlider
    {
        private readonly IDataBaseContext _context;
        public RemoveSlider(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long Id)
        {
            var slider = _context.Sliders.Find(Id);
            if (slider == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعاتی یافت نشد"
                };
            }
            slider.RemoveTime = DateTime.Now;
            slider.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت حذف شد"
            };
        }
    }
}
