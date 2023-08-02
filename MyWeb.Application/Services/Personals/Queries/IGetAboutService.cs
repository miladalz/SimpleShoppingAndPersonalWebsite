using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetAboutService
    {
        ResultDto<List<AboutList>> Execute();
        ResultDto<AboutPage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        AboutList ExecuteForEdit(long id);
    }
    public class GetAboutService : IGetAboutService
    {
        private readonly IDataBaseContext _context;
        public GetAboutService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<AboutList>> Execute()
        {
            var abouts = _context.Abouts
                .Include(p => p.TaskItems)
                .OrderByDescending(p => p.Id)
                .Select(p => new AboutList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Title2 = p.Title2,
                    Description2 = p.Description2,
                    TaskTitle = p.TaskTitle,
                    Image = p.Image,
                    HeaderImage = p.HeaderImage,
                    AboutTasks = p.TaskItems.ToList()
                    .Select(c => new AboutTask { Title = c.Title }).ToList()
                }).ToList();
            return new ResultDto<List<AboutList>>()
            {
                Data = abouts,
                IsSuccess = true,
            };
        }
        public ResultDto<AboutPage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Abouts
                    .Include(p => p.TaskItems)
                    .ToPaged(Page, PageSize, out rowCount)
                    .Select(p => new AboutList
                    {
                        Id = p.Id,
                        Description = p.Description,
                        Link = p.Link,
                        Title = p.Title,
                        Title2 = p.Title2,
                        Description2 = p.Description2,
                        TaskTitle = p.TaskTitle,
                        Image = p.Image,
                        HeaderImage = p.HeaderImage,
                        AboutTasks = p.TaskItems.ToList()
                        .Select(c => new AboutTask { Title = c.Title }).ToList()
                    }).ToList();

            return new ResultDto<AboutPage>()
            {
                Data = new AboutPage()
                {
                    Abouts = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }
        public AboutList ExecuteForEdit(long id)
        {
            var abouts = _context.Abouts
                .Include(p => p.TaskItems)
                .Where(p => p.Id == id)
                .Select(p => new AboutList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Link = p.Link,
                    Title = p.Title,
                    Title2 = p.Title2,
                    Description2 = p.Description2,
                    TaskTitle = p.TaskTitle,
                    Image = p.Image,
                    HeaderImage = p.HeaderImage,
                    AboutTasks = p.TaskItems.ToList()
                    .Select(c => new AboutTask { Title = c.Title, Id = c.Id }).ToList()
                }).FirstOrDefault();
            return abouts;
        }
    }

    public class AboutPage : ResultPage
    {
        public List<AboutList> Abouts { get; set; }
    }
    public class AboutList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Title2 { get; set; }
        public string Description2 { get; set; }
        public string TaskTitle { get; set; }
        public string Image { get; set; }
        public string HeaderImage { get; set; }
        public List<AboutTask> AboutTasks { get; set; }
    }
    public class AboutTask
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
