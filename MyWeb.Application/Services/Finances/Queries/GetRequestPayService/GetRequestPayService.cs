using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Finances.Queries.GetRequestPayService
{
    public class GetRequestPayService : IGetRequestPayService
    {
        private readonly IDataBaseContext _context;
        public GetRequestPayService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<RequestPayDto> Execute(Guid guid)
        {
            var requestPay = _context.RequestPays.Where(p => p.Guid == guid).FirstOrDefault();

            if (requestPay != null)
            {
                return new ResultDto<RequestPayDto>()
                {
                    Data = new RequestPayDto()
                    {
                        Id= requestPay.Id,
                        Amount = requestPay.Amount,
                    }
                };
            }
            else
            {
                throw new Exception("request pay not found");
            }
        }
    }
}
