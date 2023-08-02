using MyWeb.Application.Services.Finances.Commands.AddRequestPay;
using MyWeb.Application.Services.Finances.Commands.RemovePay;
using MyWeb.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using MyWeb.Application.Services.Finances.Queries.GetRequestPayService;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface IFinancesFacad
    {
        IAddRequestPayService AddRequestPayService { get; }
        IGetRequestPayForAdminService GetRequestPayForAdminService { get; }
        IGetRequestPayService GetRequestPayService { get; }
        IRemovePay RemovePay { get; }
    }
}
