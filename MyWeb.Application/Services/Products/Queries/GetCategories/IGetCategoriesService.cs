using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Products.Queries.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<CategoryPage> Execute(long? ParentId, int Page = 1, int PageSize = 10);
    }

    public class CategoryPage : ResultPage
    {
        public List<CategoriesDto> Categories { get; set; }
    }

    public class CategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }
        public ParentCategoryDto Parent { get; set; }

    }

    public class ParentCategoryDto
    {
        public long Id { get; set; }
        public string name { get; set; }
    }
}