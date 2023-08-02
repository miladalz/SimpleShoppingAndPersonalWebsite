using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetPriceService
    {
        ResultDto<List<PriceList>> Execute();
        ResultDto<PricePage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        PriceList ExecuteForEdit(long id);
    }
    public class GetPriceService : IGetPriceService
    {
        private readonly IDataBaseContext _context;
        public GetPriceService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<PriceList>> Execute()
        {
            var prices = _context.Prices
                .Include(p => p.PriceItems)
                .ToList()
                .Select(p => new PriceList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    PriceItems = p.PriceItems
                    .ToList()
                    .Select(c => new Price
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Amount = c.Amount,
                        Link = c.Link,
                        Title = c.Title,
                        Featured = c.Featured
                    }).ToList()
                }).ToList();
            return new ResultDto<List<PriceList>>()
            {
                Data = prices,
                IsSuccess = true,
            };
        }
        public ResultDto<PricePage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Prices
                .Include(p => p.PriceItems)
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new PriceList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    PriceItems = p.PriceItems
                    .ToList()
                    .Select(c => new Price
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Amount = c.Amount,
                        Link = c.Link,
                        Title = c.Title,
                        Featured = c.Featured
                    }).ToList()
                }).ToList();
            return new ResultDto<PricePage>()
            {
                Data = new PricePage()
                {
                    Prices = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public PriceList ExecuteForEdit(long id)
        {
            var prices = _context.Prices
                .Include(p => p.PriceItems)
                .Where(p => p.Id == id)
                .Select(p => new PriceList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    PriceItems = p.PriceItems
                    .ToList()
                    .Select(c => new Price
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Amount = c.Amount,
                        Link = c.Link,
                        Title = c.Title,
                        Featured = c.Featured
                    }).ToList()
                }).FirstOrDefault();
            return prices;
        }
    }
    public class PricePage : ResultPage
    {
        public List<PriceList> Prices { get; set; }
    }
    public class PriceList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Price> PriceItems { get; set; }
    }

    public class Price
    {
        public long Id { get; set; }
        public long Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool Featured { get; set; }
    }
}
