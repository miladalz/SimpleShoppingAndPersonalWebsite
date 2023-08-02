using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using Microsoft.EntityFrameworkCore;
using MyWeb.Common;

namespace MyWeb.Application.Services.Products.Queries.GetCategories
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext _context;

        public GetCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<CategoryPage> Execute(long? ParentId, int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var categories = _context.Categories
               .Include(p => p.ParentCategory)
               .Include(p => p.SubCategories)
               .Where(p => p.ParentCategoryId == ParentId)
               .ToPaged(Page, PageSize, out rowCount)
               .ToList()
               .Select(p => new CategoriesDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   Parent = p.ParentCategory != null ? new ParentCategoryDto
                   {
                       Id = p.ParentCategory.Id,
                       name = p.ParentCategory.Name,
                   }
                   : null,
                   HasChild = p.SubCategories.Count() > 0 ? true : false,
               }).ToList();            

            return new ResultDto<CategoryPage>()
            {
                Data = new CategoryPage()
                {
                    Categories = categories,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "لیست با موقیت برگشت داده شد",
            };
        }
    }
}



