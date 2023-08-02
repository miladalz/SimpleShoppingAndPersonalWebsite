using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Products.Commands.RemoveProduct
{
    public interface IRemoveProduct
    {
        ResultDto Execute(long Id);
    }

    public class RemoveProduct : IRemoveProduct
    {
        private readonly IDataBaseContext _context;
        public RemoveProduct(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long Id)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعاتی یافت نشد"
                };
            }
            product.RemoveTime = DateTime.Now;
            product.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت حذف شد"
            };
        }
    }
}
