using MyWeb.Application.Services.Orders.Command.AddNewOrder;
using MyWeb.Application.Services.Orders.Command.RemoveOrder;
using MyWeb.Application.Services.Orders.Queries.GetOrdersForAdmin;
using MyWeb.Application.Services.Orders.Queries.GetUserOrders;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface IOrderFacad
    {
        IAddNewOrderService AddNewOrderService { get; }
        IGetOrdersForAdminService GetOrdersForAdminService { get; }
        IGetUserOrdersService GetUserOrdersService { get; }
        IRemoveOrder RemoveOrder { get; }
    }
}
