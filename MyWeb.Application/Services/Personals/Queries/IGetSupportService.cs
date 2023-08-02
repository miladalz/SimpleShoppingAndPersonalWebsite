using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetSupportService
	{
        ResultDto<List<SupportList>> Execute();
        ResultDto<SupportPage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        SupportList ExecuteForEdit(long id);
    }

    public class GetSupportService : IGetSupportService
    {
        private readonly IDataBaseContext _context;
        public GetSupportService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SupportList>> Execute()
        {
            var supports = _context.Supports
                .OrderByDescending(p => p.Id)
                .Select(p => new SupportList
                {
                    Id = p.Id,
                    LinkTitle = p.LinkTitle,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image,
                    Location = p.Location
                }).ToList();
            return new ResultDto<List<SupportList>>()
            {
                Data = supports,
                IsSuccess = true,
            };
        }
        public ResultDto<SupportPage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Supports
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new SupportList
                {
                    Id = p.Id,
                    LinkTitle = p.LinkTitle,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image,
                    Location = p.Location
                }).ToList();
            return new ResultDto<SupportPage>()
            {
                Data = new SupportPage()
                {
                    Supports = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public SupportList ExecuteForEdit(long id)
        {
            var supports = _context.Supports
               .Where(p => p.Id == id)
               .Select(p => new SupportList
               {
                   Id = p.Id,
                   LinkTitle = p.LinkTitle,
                   Link = p.Link,
                   Title = p.Title,
                   Image = p.Image,
                   Location = p.Location
               }).FirstOrDefault();
            return supports;
        }
    }
    public class SupportPage : ResultPage
    {
        public List<SupportList> Supports { get; set; }
    }
    public class SupportList
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string LinkTitle { get; set; }
    }
}
