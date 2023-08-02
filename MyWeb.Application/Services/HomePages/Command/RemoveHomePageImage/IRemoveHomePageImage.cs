using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.HomePages.Command.RemoveHomePageImage
{
    public interface IRemoveHomePageImage
    {
        ResultDto Execute(long Id);
    }

    public class RemoveHomePageImage : IRemoveHomePageImage
    {
        private readonly IDataBaseContext _context;
        public RemoveHomePageImage(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long Id)
        {
            var homePageImage = _context.HomePageImages.Find(Id);
            if (homePageImage == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعاتی یافت نشد"
                };
            }
            homePageImage.RemoveTime = DateTime.Now;
            homePageImage.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت حذف شد"
            };
        }
    }
}
