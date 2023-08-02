using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using Microsoft.EntityFrameworkCore;
using MyWeb.Common;

namespace MyWeb.Application.Services.Finances.Queries.GetRequestPayForAdmin
{
    public class GetRequestPayForAdminService : IGetRequestPayForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetRequestPayForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<PayPage> Execute(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var pays = _context.RequestPays
                .Include(p => p.User)
                .ToPaged(Page, PageSize, out rowCount)
                .ToList()
                .Select(p => new RequestPayDto
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    Authority = p != null ? p.Authority : "",
                    Guid = p.Guid,
                    IsPay = p.IsPay,
                    PayDate = p.PayDate,
                    RefId = p.RefId,
                    UserId = p.UserId,
                    UserName = p.User.FullName
                }).ToList();

            return new ResultDto<PayPage>()
            {
                Data = new PayPage()
                {
                    Pays = pays,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "لیست با موقیت برگشت داده شد",
            };
        }
    }
}
