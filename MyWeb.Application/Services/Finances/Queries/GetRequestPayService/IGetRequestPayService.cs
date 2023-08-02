using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Finances.Queries.GetRequestPayService
{
    public interface IGetRequestPayService
    {
        ResultDto<RequestPayDto> Execute(Guid guid);
    }

    public class RequestPayDto
    {
        public long Id { get; set; }
        public int Amount { get; set; }

    }
}
