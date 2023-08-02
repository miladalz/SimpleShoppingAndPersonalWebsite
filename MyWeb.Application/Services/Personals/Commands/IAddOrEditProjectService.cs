using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditProjectService
    {
        ResultDto ExecuteAdd(ProjectDto projectDto);
        ResultDto ExecuteEdit(ProjectDto projectDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditProjectService : IAddOrEditProjectService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditProjectService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(ProjectDto projectDto)
        {
            var project = new Project()
            {
                Title = projectDto.Title,
                Description = projectDto.Description
            };
            _context.Projects.Add(project);

            List<ProjectItem> projectItems = new();
            foreach (var item in projectDto.ProjectItems)
            {
                projectItems.Add(new ProjectItem
                {
                    Description = item.Description,
                    Image = item.Image,
                    Link = item.Link,
                    ProjectType = item.ProjectType,
                    Title = item.Title,
                    Project = project
                });
            }
            _context.ProjectItems.AddRange(projectItems);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(ProjectDto projectDto)
        {
            var project = _context.Projects
                .Include(p => p.ProjectItems)
                .FirstOrDefault(p => p.Id == projectDto.Id);
            if (project != null)
            {
                project.Title = projectDto.Title;
                project.Description = projectDto.Description;
                project.UpdateTime = DateTime.Now;

                foreach (var item in projectDto.ProjectItems)
                {
                    var existItem = project.ProjectItems.FirstOrDefault(a => a.Id == item.Id);
                    if (existItem != null)
                    {
                        existItem.Description = item.Description;
                        existItem.Image = item.Image;
                        existItem.Link = item.Link;
                        existItem.ProjectType = item.ProjectType;
                        existItem.Title = item.Title;
                        existItem.UpdateTime = DateTime.Now;
                    }
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "با موفقیت ذخیره شد",
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "رکوردی یافت نشد.",
                };
            }
        }

        public ResultDto ExecuteDelete(long id)
        {
            var project = _context.Projects
                .Include(p => p.ProjectItems)
                .FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            project.RemoveTime = DateTime.Now;
            project.IsRemoved = true;
            foreach (var item in project.ProjectItems)
            {
                item.RemoveTime = DateTime.Now;
                item.IsRemoved = true;
            }
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
    public class ProjectDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ProjectItemDto> ProjectItems { get; set; }
    }

    public class ProjectItemDto
    {
        public long Id { get; set; }
        public string ProjectType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }
}
