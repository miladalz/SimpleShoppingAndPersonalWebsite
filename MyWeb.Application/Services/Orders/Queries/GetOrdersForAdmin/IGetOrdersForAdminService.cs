using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Orders;

namespace MyWeb.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public interface IGetOrdersForAdminService
    {
        ResultDto<OrderPage> Execute(OrderState orderState, int Page = 1, int PageSize = 10);
    }

    public class OrderPage : ResultPage
    {
        public List<OrdersDto> Orders { get; set; }
    }

    public class OrdersDto
    {
        public long OrderId { get; set; }
        public DateTime InsetTime { get; set; }
        public long RequestId { get; set; }
        public long UserId { get; set; }
        public OrderState OrderState { get; set; }
        public int ProductCount { get; set; }

    }
}
