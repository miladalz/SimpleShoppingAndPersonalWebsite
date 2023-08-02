using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.HomePages.Queries.GetSildersForAdmin
{
    public class GetSildersForAdmin : IGetSildersForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetSildersForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<SilderPage> Execute(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var silders = _context.Sliders
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new Slider
                {
                    Link = p.link,
                    Src = p.Src,
                    Id = p.Id
                }).ToList();
            return new ResultDto<SilderPage>()
            {
                Data = new SilderPage()
                {
                    Sliders = silders,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }
    }
}
