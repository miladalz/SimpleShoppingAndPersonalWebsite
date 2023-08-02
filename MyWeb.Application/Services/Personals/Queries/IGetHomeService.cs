using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetHomeService
    {
        ResultDto<List<HomeList>> Execute();
        ResultDto<HomePage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        HomeList ExecuteForEdit(long id);
    }
    public class GetHomeService : IGetHomeService
    {
        private readonly IDataBaseContext _context;
        public GetHomeService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<HomeList>> Execute()
        {
            var homes = _context.Homes
                .OrderByDescending(p => p.Id)
                .Select(p => new HomeList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image
                }).ToList();
            return new ResultDto<List<HomeList>>()
            {
                Data = homes,
                IsSuccess = true,
            };
        }
        public ResultDto<HomePage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Homes
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new HomeList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image
                }).ToList();
            return new ResultDto<HomePage>()
            {
                Data = new HomePage()
                {
                    Homes = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public HomeList ExecuteForEdit(long id)
        {
            var homes = _context.Homes
                .Where(p => p.Id == id)
                .Select(p => new HomeList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Image = p.Image
                }).FirstOrDefault();
            return homes;
        }
    }
    public class HomePage : ResultPage
    {
        public List<HomeList> Homes { get; set; }
    }
    public class HomeList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }
}
