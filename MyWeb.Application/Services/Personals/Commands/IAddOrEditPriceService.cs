using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditPriceService
    {
        ResultDto ExecuteAdd(PriceDto priceDto);
        ResultDto ExecuteEdit(PriceDto priceDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditPriceService : IAddOrEditPriceService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditPriceService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(PriceDto priceDto)
        {
            var price = new Price()
            {
                Title = priceDto.Title,
                Description = priceDto.Description
            };
            _context.Prices.Add(price);

            List<PriceItem> priceItems = new();
            foreach (var item in priceDto.PriceItems)
            {
                priceItems.Add(new PriceItem
                {
                    Amount = item.Amount,
                    Description = item.Description,
                    Featured = item.Featured,
                    Link = item.Link,
                    Title = item.Title,
                    Price = price
                });
            }
            _context.PriceItems.AddRange(priceItems);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(PriceDto priceDto)
        {
            var price = _context.Prices
                .Include(p => p.PriceItems)
                .FirstOrDefault(p => p.Id == priceDto.Id);
            if (price != null)
            {
                price.Title = priceDto.Title;
                price.Description = priceDto.Description;
                price.UpdateTime = DateTime.Now;

                foreach (var item in priceDto.PriceItems)
                {
                    var existItem = price.PriceItems.FirstOrDefault(a => a.Id == item.Id);
                    if (existItem != null)
                    {
                        existItem.Amount = item.Amount;
                        existItem.Description = item.Description;
                        existItem.Featured = item.Featured;
                        existItem.Link = item.Link;
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
            var price = _context.Prices
                .Include(p => p.PriceItems)
                .FirstOrDefault(p => p.Id == id);
            if (price == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            price.RemoveTime = DateTime.Now;
            price.IsRemoved = true;
            foreach (var item in price.PriceItems)
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
    public class PriceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PriceItemDto> PriceItems { get; set; }
    }
    public class PriceItemDto
    {
        public long Id { get; set; }
        public long Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool Featured { get; set; }
    }
}
