using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Finances.Commands.RemovePay
{
    public interface IRemovePay
    {
        ResultDto Execute(long Id);
    }

    public class RemovePay : IRemovePay
    {
        private readonly IDataBaseContext _context;
        public RemovePay(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long Id)
        {
            var pay = _context.RequestPays.Find(Id);
            if (pay == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعاتی یافت نشد"
                };
            }
            pay.RemoveTime = DateTime.Now;
            pay.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت حذف شد"
            };
        }
    }
}
