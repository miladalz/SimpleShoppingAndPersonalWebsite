using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetProjectService
    {
        ResultDto<List<ProjectList>> Execute();
        ResultDto<ProjectPage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        ProjectList ExecuteForEdit(long id);
    }
    public class GetProjectService : IGetProjectService
    {
        private readonly IDataBaseContext _context;
        public GetProjectService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ProjectList>> Execute()
        {
            var projects = _context.Projects
                .Include(p => p.ProjectItems)
                .ToList()
                .Select(p => new ProjectList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ProjectItems = p.ProjectItems
                    .ToList()
                    .Select(c => new Project
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Title = c.Title,
                        ProjectType = c.ProjectType,
                        Link = c.Link,
                        Image = c.Image
                    }).ToList()
                }).ToList();
            return new ResultDto<List<ProjectList>>()
            {
                Data = projects,
                IsSuccess = true,
            };
        }
        public ResultDto<ProjectPage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Projects
                .Include(p => p.ProjectItems)
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new ProjectList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ProjectItems = p.ProjectItems
                    .ToList()
                    .Select(c => new Project
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Title = c.Title,
                        ProjectType = c.ProjectType,
                        Link = c.Link,
                        Image = c.Image
                    }).ToList()
                }).ToList();
            return new ResultDto<ProjectPage>()
            {
                Data = new ProjectPage()
                {
                    Projects = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public ProjectList ExecuteForEdit(long id)
        {
            var projects = _context.Projects
                .Include(p => p.ProjectItems)
                .Where(p => p.Id == id)
                .Select(p => new ProjectList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ProjectItems = p.ProjectItems
                    .ToList()
                    .Select(c => new Project
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Title = c.Title,
                        ProjectType = c.ProjectType,
                        Link = c.Link,
                        Image = c.Image
                    }).ToList()
                }).FirstOrDefault();
            return projects;
        }
    }
    public class ProjectPage : ResultPage
    {
        public List<ProjectList> Projects { get; set; }
    }
    public class ProjectList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Project> ProjectItems { get; set; }
    }
    public class Project
    {
        public long Id { get; set; }
        public string ProjectType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }
}
