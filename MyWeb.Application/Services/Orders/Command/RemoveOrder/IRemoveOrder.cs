using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Orders.Command.RemoveOrder
{
    public interface IRemoveOrder
    {
        ResultDto Execute(long Id);
    }

    public class RemoveOrder : IRemoveOrder
    {
        private readonly IDataBaseContext _context;
        public RemoveOrder(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long Id)
        {
            var order = _context.Orders.Find(Id);
            if (order == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعاتی یافت نشد"
                };
            }
            order.RemoveTime = DateTime.Now;
            order.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت حذف شد"
            };
        }
    }
}
