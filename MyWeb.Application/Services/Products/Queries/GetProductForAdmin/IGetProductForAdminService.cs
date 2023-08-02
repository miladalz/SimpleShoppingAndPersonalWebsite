using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductForAdminPage> Execute(int Page = 1, int PageSize = 10);
    }

    public class ProductForAdminPage : ResultPage
    {
        public List<ProductsFormAdminList_Dto> Products { get; set; }
    }

    public class ProductsFormAdminList_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
    }
}
