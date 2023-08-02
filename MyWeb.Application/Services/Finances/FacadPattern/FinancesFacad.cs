using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Finances.Commands.AddRequestPay;
using MyWeb.Application.Services.Finances.Commands.RemovePay;
using MyWeb.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using MyWeb.Application.Services.Finances.Queries.GetRequestPayService;

namespace MyWeb.Application.Services.Finances.FacadPattern
{
    public class FinancesFacad : IFinancesFacad
    {
        private readonly IDataBaseContext _context;
        public FinancesFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IAddRequestPayService _addRequestPayService;
        public IAddRequestPayService AddRequestPayService
        {
            get
            {
                return _addRequestPayService ??= new AddRequestPayService(_context);
            }
        }

        private IGetRequestPayForAdminService _getRequestPayForAdminService;
        public IGetRequestPayForAdminService GetRequestPayForAdminService
        {
            get
            {
                return _getRequestPayForAdminService ??= new GetRequestPayForAdminService(_context);
            }
        }

        private IGetRequestPayService _getRequestPayService;
        public IGetRequestPayService GetRequestPayService
        {
            get
            {
                return _getRequestPayService ??= new GetRequestPayService(_context);
            }
        }

        private IRemovePay _removePay;
        public IRemovePay RemovePay
        {
            get
            {
                return _removePay ??= new RemovePay(_context);
            }
        }
    }
}
