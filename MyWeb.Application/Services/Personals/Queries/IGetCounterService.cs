using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetCounterService
    {
        ResultDto<List<CounterList>> Execute();
        ResultDto<CounterPage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        CounterList ExecuteForEdit(long id);
    }

    public class GetCounterService : IGetCounterService
    {
        private readonly IDataBaseContext _context;
        public GetCounterService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<CounterList>> Execute()
        {
            var items = _context.Counters
                .OrderByDescending(p => p.Id)
                .Select(p => new CounterList
                {
                    Id = p.Id,
                    Title = p.Title,
                    Image = p.Image,
                    Icon = p.Icon,
                    Count = p.Count
                }).ToList();
            return new ResultDto<List<CounterList>>()
            {
                Data = items,
                IsSuccess = true,
            };
        }
        public ResultDto<CounterPage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Counters
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new CounterList
                {
                    Id = p.Id,
                    Title = p.Title,
                    Image = p.Image,
                    Icon = p.Icon,
                    Count = p.Count
                }).ToList();
            return new ResultDto<CounterPage>()
            {
                Data = new CounterPage()
                {
                    Counters = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public CounterList ExecuteForEdit(long id)
        {
            var counter = _context.Counters
                .Where(p => p.Id == id)
                .Select(p => new CounterList
                {
                    Id = p.Id,
                    Title = p.Title,
                    Image = p.Image,
                    Icon = p.Icon,
                    Count = p.Count
                }).FirstOrDefault();
            return counter;
        }
    }
    public class CounterPage : ResultPage
    {
        public List<CounterList> Counters { get; set; }
    }
    public class CounterList
    {
        public long Id { get; set; }
        public string Icon { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
