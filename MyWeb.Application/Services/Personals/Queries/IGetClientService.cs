using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetClientService
    {
        ResultDto<List<ClientList>> Execute();
        ResultDto<ClientPage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        ClientList ExecuteForEdit(long id);
    }
    public class GetClientService : IGetClientService
    {
        private readonly IDataBaseContext _context;
        public GetClientService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ClientList>> Execute()
        {
            var clinets = _context.Clinets
                .OrderByDescending(p => p.Id)
                .Select(p => new ClientList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image,
                    Comment = p.Comment,
                    TitleImage = p.TitleImage
                }).ToList();
            return new ResultDto<List<ClientList>>()
            {
                Data = clinets,
                IsSuccess = true,
            };
        }
        public ResultDto<ClientPage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Clinets
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new ClientList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image,
                    Comment = p.Comment,
                    TitleImage = p.TitleImage
                }).ToList();
            return new ResultDto<ClientPage>()
            {
                Data = new ClientPage()
                {
                    Clients = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public ClientList ExecuteForEdit(long id)
        {
            var client = _context.Clinets
                .Where(p => p.Id == id)
                .Select(p => new ClientList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image,
                    Comment = p.Comment,
                    TitleImage = p.TitleImage
                }).FirstOrDefault();
            return client;
        }
    }

    public class ClientPage : ResultPage
    {
        public List<ClientList> Clients { get; set; }
    }
    public class ClientList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string Comment { get; set; }
        public string TitleImage { get; set; }
    }
}
