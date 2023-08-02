using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;
using static System.Net.Mime.MediaTypeNames;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditHomeService
    {
        ResultDto ExecuteAdd(HomeDto homeDto);
        ResultDto ExecuteEdit(HomeDto homeDto);
        ResultDto ExecuteDelete(long id);
    }

    public class AddOrEditHomeService : IAddOrEditHomeService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditHomeService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(HomeDto homeDto)
        {
            _context.Homes.Add(new Home()
            {
                Title = homeDto.Title,
                Description = homeDto.Description,
                Link = homeDto.Link,
                Image = homeDto.Image
            });
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(HomeDto homeDto)
        {
            var home = _context.Homes                
                .FirstOrDefault(p => p.Id == homeDto.Id);
            if (home != null)
            {
                home.Title = homeDto.Title;
                home.Description = homeDto.Description;
                home.Link = homeDto.Link;
                home.Image = homeDto.Image;
                home.UpdateTime = DateTime.Now;

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
            var home = _context.Homes
                .FirstOrDefault(p => p.Id == id);
            if (home == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            home.RemoveTime = DateTime.Now;
            home.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }

    public class HomeDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }
}
