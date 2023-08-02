using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Products.Commands.AddNewCategory;
using MyWeb.Application.Services.Products.Commands.AddNewProduct;
using MyWeb.Application.Services.Products.Queries.GetAllCategories;
using MyWeb.Application.Services.Products.Queries.GetCategories;
using MyWeb.Application.Services.Products.Queries.GetProductDetailForAdmin;
using MyWeb.Application.Services.Products.Queries.GetProductDetailForSite;
using MyWeb.Application.Services.Products.Queries.GetProductForAdmin;
using MyWeb.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Hosting;
using MyWeb.Application.Services.Products.Commands.RemoveCategory;
using MyWeb.Application.Services.Products.Commands.RemoveProduct;

namespace MyWeb.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;

        }

        private IAddNewCategoryService _addNewCategory;
        public IAddNewCategoryService AddNewCategoryService
        {
            get
            {
                return _addNewCategory ??= new AddNewCategoryService(_context);
            }
        }

        private IGetCategoriesService _getCategoriesService;
        public IGetCategoriesService GetCategoriesService
        {
            get
            {
                return _getCategoriesService ??= new GetCategoriesService(_context);
            }
        }

        private IAddNewProductService _addNewProductService;
        public IAddNewProductService AddNewProductService
        {
            get
            {
                return _addNewProductService ??= new AddNewProductService(_context, _environment);
            }
        }

        private IGetAllCategoriesService _getAllCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _getAllCategoriesService ??= new GetAllCategoriesService(_context);
            }
        }

        private IGetProductForAdminService _getProductForAdminService;
        public IGetProductForAdminService GetProductForAdminService
        {
            get
            {
                return _getProductForAdminService ??= new GetProductForAdminService(_context);
            }
        }

        private IGetProductDetailForAdminService _getProductDetailForAdminService;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get
            {
                return _getProductDetailForAdminService ??= new GetProductDetailForAdminService(_context);
            }
        }    
        
        private IGetProductForSiteService   _getProductForSiteService;
        public IGetProductForSiteService  GetProductForSiteService
        {
            get
            {
                return _getProductForSiteService ??= new GetProductForSiteService(_context);
            }
        }    
                
        private IGetProductDetailForSiteService  _getProductDetailForSiteService;
        public IGetProductDetailForSiteService  GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService ??= new GetProductDetailForSiteService(_context);
            }
        }

        private IRemoveCategory _removeCategory;
        public IRemoveCategory RemoveCategory
        {
            get
            {
                return _removeCategory ??= new RemoveCategory(_context);
            }
        }

        private IRemoveProduct _removeProduct;
        public IRemoveProduct RemoveProduct
        {
            get
            {
                return _removeProduct ??= new RemoveProduct(_context);
            }
        }
    }
}
