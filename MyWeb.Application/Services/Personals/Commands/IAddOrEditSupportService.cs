using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditSupportService
    {
        ResultDto ExecuteAdd(SupportDto supportDto);
        ResultDto ExecuteEdit(SupportDto supportDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditSupportService : IAddOrEditSupportService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditSupportService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(SupportDto supportDto)
        {
            var support = new Support()
            {
                Image = supportDto.Image,
                Link= supportDto.Link,
                LinkTitle=supportDto.LinkTitle,
                Location= supportDto.Location,
                Title=supportDto.Title               
            };
            _context.Supports.Add(support);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(SupportDto supportDto)
        {
            var support = _context.Supports
                .FirstOrDefault(p => p.Id == supportDto.Id);
            if (support != null)
            {
                support.Image = supportDto.Image;
                support.Link = supportDto.Link;
                support.LinkTitle = supportDto.LinkTitle;
                support.Location = supportDto.Location;
                support.Title = supportDto.Title;
                support.UpdateTime = DateTime.Now;

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
            var support = _context.Supports
                .FirstOrDefault(p => p.Id == id);
            if (support == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            support.RemoveTime = DateTime.Now;
            support.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
    public class SupportDto
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string LinkTitle { get; set; }
    }
}
