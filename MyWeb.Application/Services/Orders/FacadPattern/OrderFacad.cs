using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Orders.Command.AddNewOrder;
using MyWeb.Application.Services.Orders.Command.RemoveOrder;
using MyWeb.Application.Services.Orders.Queries.GetOrdersForAdmin;
using MyWeb.Application.Services.Orders.Queries.GetUserOrders;

namespace MyWeb.Application.Services.Orders.FacadPattern
{
    public class OrderFacad : IOrderFacad
    {
        private readonly IDataBaseContext _context;
        public OrderFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IAddNewOrderService _addNewOrderService;
        public IAddNewOrderService AddNewOrderService
        {
            get
            {
                return _addNewOrderService ??= new AddNewOrderService(_context);
            }
        }

        private IGetOrdersForAdminService _getOrdersForAdminService;
        public IGetOrdersForAdminService GetOrdersForAdminService
        {
            get
            {
                return _getOrdersForAdminService ??= new GetOrdersForAdminService(_context);
            }
        }

        private IGetUserOrdersService _getUserOrdersService;
        public IGetUserOrdersService GetUserOrdersService
        {
            get
            {
                return _getUserOrdersService ??= new GetUserOrdersService(_context);
            }
        }

        private IRemoveOrder _removeOrder;
        public IRemoveOrder RemoveOrder
        {
            get
            {
                return _removeOrder ??= new RemoveOrder(_context);
            }
        }
    }
}
