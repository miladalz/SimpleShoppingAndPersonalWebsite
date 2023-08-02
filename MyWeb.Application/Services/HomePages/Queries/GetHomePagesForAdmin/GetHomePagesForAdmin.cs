using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.HomePages.Queries.GetHomePagesForAdmin
{
    public class GetHomePagesForAdmin : IGetHomePagesForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetHomePagesForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<HomePageImagePage> Execute(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var homePageImages = _context.HomePageImages
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new HomePageImage
                {
                    Link = p.link,
                    Src = p.Src,
                    ImageLocation = p.ImageLocation,
                    Id = p.Id
                }).ToList();

            return new ResultDto<HomePageImagePage>
            {
                Data = new HomePageImagePage
                {
                    HomePageImages = homePageImages,
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
