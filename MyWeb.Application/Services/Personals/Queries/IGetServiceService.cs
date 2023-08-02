using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetServiceService
    {
        ResultDto<List<ServiceList>> Execute();
        ResultDto<ServicePage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        ServiceList ExecuteForEdit(long id);
    }
    public class GetServiceService : IGetServiceService
    {
        private readonly IDataBaseContext _context;
        public GetServiceService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ServiceList>> Execute()
        {
            var services = _context.Services
                .Include(p => p.ServiceItem)
                .ToList()
                .Select(p => new ServiceList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ServiceItems = p.ServiceItem
                    .ToList()
                    .Select(c => new Service
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Title = c.Title
                    }).ToList()
                }).ToList();
            return new ResultDto<List<ServiceList>>()
            {
                Data = services,
                IsSuccess = true,
            };
        }
        public ResultDto<ServicePage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Services
                .Include(p => p.ServiceItem)
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new ServiceList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ServiceItems = p.ServiceItem
                    .ToList()
                    .Select(c => new Service
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Title = c.Title
                    }).ToList()
                }).ToList();
            return new ResultDto<ServicePage>()
            {
                Data = new ServicePage()
                {
                    Services = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public ServiceList ExecuteForEdit(long id)
        {
            var services = _context.Services
                .Include(p => p.ServiceItem)
                .Where(p => p.Id == id)
                .Select(p => new ServiceList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ServiceItems = p.ServiceItem
                    .ToList()
                    .Select(c => new Service
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Title = c.Title
                    }).ToList()
                }).FirstOrDefault();
            return services;
        }
    }
    public class ServicePage : ResultPage
    {
        public List<ServiceList> Services { get; set; }
    }
    public class ServiceList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Service> ServiceItems { get; set; }
    }

    public class Service
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
