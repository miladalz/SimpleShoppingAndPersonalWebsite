using MyWeb.Application.Services.Products.Commands.AddNewCategory;
using MyWeb.Application.Services.Products.Commands.AddNewProduct;
using MyWeb.Application.Services.Products.Commands.RemoveCategory;
using MyWeb.Application.Services.Products.Commands.RemoveProduct;
using MyWeb.Application.Services.Products.Queries.GetAllCategories;
using MyWeb.Application.Services.Products.Queries.GetCategories;
using MyWeb.Application.Services.Products.Queries.GetProductDetailForAdmin;
using MyWeb.Application.Services.Products.Queries.GetProductDetailForSite;
using MyWeb.Application.Services.Products.Queries.GetProductForAdmin;
using MyWeb.Application.Services.Products.Queries.GetProductForSite;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        IAddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }
        IAddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }
        IGetProductForAdminService GetProductForAdminService { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }
        IGetProductForSiteService GetProductForSiteService { get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
        IRemoveCategory RemoveCategory { get; }
        IRemoveProduct RemoveProduct { get; }
    }
}
