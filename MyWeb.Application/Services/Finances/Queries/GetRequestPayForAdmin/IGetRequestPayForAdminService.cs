using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Finances.Queries.GetRequestPayForAdmin
{
    public interface IGetRequestPayForAdminService
    {
        ResultDto<PayPage> Execute(int Page = 1, int PageSize = 10);
    }

    public class PayPage : ResultPage
    {
        public List<RequestPayDto> Pays { get; set; }
    }

    public class RequestPayDto
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }
}
