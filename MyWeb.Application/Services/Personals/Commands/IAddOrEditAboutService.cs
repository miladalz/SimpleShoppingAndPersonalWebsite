using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditAboutService
    {
        ResultDto ExecuteAdd(AboutDto aboutDto);
        ResultDto ExecuteEdit(AboutDto aboutDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditAboutService : IAddOrEditAboutService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditAboutService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(AboutDto aboutDto)
        {
            var about = new About()
            {
                Title = aboutDto.Title,
                Description = aboutDto.Description,
                Link = aboutDto.Link,
                Description2 = aboutDto.Description2,
                HeaderImage = aboutDto.HeaderImage,
                Image = aboutDto.Image,
                TaskTitle = aboutDto.TaskTitle,
                Title2 = aboutDto.Title2
            };
            _context.Abouts.Add(about);

            List<AboutTask> aboutTasks = new();
            foreach (var item in aboutDto.AboutTasks)
            {
                aboutTasks.Add(new AboutTask
                {
                    Title = item.Title,
                    About = about
                });
            }
            _context.Tasks.AddRange(aboutTasks);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }
        public ResultDto ExecuteEdit(AboutDto aboutDto)
        {
            var about = _context.Abouts
                .Include(p => p.TaskItems)
                .FirstOrDefault(p => p.Id == aboutDto.Id);
            if (about != null)
            {
                about.Title = aboutDto.Title;
                about.Description = aboutDto.Description;
                about.Link = aboutDto.Link;
                about.Description2 = aboutDto.Description2;
                about.HeaderImage = aboutDto.HeaderImage;
                about.Image = aboutDto.Image;
                about.TaskTitle = aboutDto.TaskTitle;
                about.Title2 = aboutDto.Title2;
                about.UpdateTime = DateTime.Now;

                foreach (var item in aboutDto.AboutTasks)
                {
                    var existItem = about.TaskItems.FirstOrDefault(a => a.Id == item.Id);
                    if (existItem != null)
                    {
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
            var about = _context.Abouts
                .Include(p => p.TaskItems)
                .FirstOrDefault(p => p.Id == id);
            if (about == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            about.RemoveTime = DateTime.Now;
            about.IsRemoved = true;
            foreach (var item in about.TaskItems)
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

    public class AboutDto
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
        public List<AboutTaskDto> AboutTasks { get; set; }
    }
    public class AboutTaskDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
