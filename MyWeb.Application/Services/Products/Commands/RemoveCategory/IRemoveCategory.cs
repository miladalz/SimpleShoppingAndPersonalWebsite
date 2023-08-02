using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Products.Commands.RemoveCategory
{
    public interface IRemoveCategory
    {
        ResultDto Execute(long Id);
    }

    public class RemoveCategory : IRemoveCategory
    {
        private readonly IDataBaseContext _context;
        public RemoveCategory(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long Id)
        {
            var category = _context.Categories.Find(Id);
            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعاتی یافت نشد"
                };
            }
            category.RemoveTime = DateTime.Now;
            category.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت حذف شد"
            };
        }
    }
}
