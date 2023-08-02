using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using MyWeb.Common;

namespace MyWeb.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public class GetOrdersForAdminService : IGetOrdersForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetOrdersForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<OrderPage> Execute(OrderState orderState, int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var orders = _context.Orders
                 .Include(p => p.OrderDetails)
                 .Where(p => p.OrderState == orderState)
                 .OrderByDescending(p => p.Id)
                 .ToPaged(Page, PageSize, out rowCount)
                 .ToList()
                 .Select(p => new OrdersDto
                 {
                     InsetTime = p.InsertTime,
                     OrderId = p.Id,
                     OrderState = p.OrderState,
                     ProductCount = p.OrderDetails.Count(),
                     RequestId = p.RequestPayId,
                     UserId = p.UserId,
                 }).ToList();
            
            return new ResultDto<OrderPage>()
            {
                Data = new OrderPage()
                {
                    Orders = orders,
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
