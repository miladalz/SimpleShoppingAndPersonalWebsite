using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditCounterService
    {
        ResultDto ExecuteAdd(CounterDto counterDto);
        ResultDto ExecuteEdit(CounterDto counterDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditCounterService : IAddOrEditCounterService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditCounterService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto ExecuteAdd(CounterDto counterDto)
        {
            var couter = new Counter()
            {
                Title = counterDto.Title,
                Count = counterDto.Count,
                Icon = counterDto.Icon,
                Image = counterDto.Image
            };
            _context.Counters.Add(couter);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(CounterDto counterDto)
        {
            var counter = _context.Counters                
                .FirstOrDefault(p => p.Id == counterDto.Id);
            if (counter != null)
            {
                counter.Title = counterDto.Title;
                counter.Count = counterDto.Count;
                counter.Icon = counterDto.Icon;
                counter.Image = counterDto.Image;
                counter.UpdateTime = DateTime.Now;

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
            var counter = _context.Counters                
                .FirstOrDefault(p => p.Id == id);
            if (counter == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            counter.RemoveTime = DateTime.Now;
            counter.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
    public class CounterDto
    {
        public long Id { get; set; }
        public string Icon { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
