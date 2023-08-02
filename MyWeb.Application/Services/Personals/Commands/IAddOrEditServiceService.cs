using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditServiceService
    {
        ResultDto ExecuteAdd(ServiceDto serviceDto);
        ResultDto ExecuteEdit(ServiceDto serviceDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditServiceService : IAddOrEditServiceService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditServiceService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(ServiceDto serviceDto)
        {
            var service = new Service()
            {
                Title = serviceDto.Title,
                Description = serviceDto.Description
            };
            _context.Services.Add(service);

            List<ServiceItem> serviceItems = new();
            foreach (var item in serviceDto.ServiceItems)
            {
                serviceItems.Add(new ServiceItem
                {
                    Description = item.Description,
                    Icon = item.Icon,
                    Title = item.Title,
                    Service = service
                });
            }
            _context.ServiceItem.AddRange(serviceItems);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(ServiceDto serviceDto)
        {
            var service = _context.Services
                .Include(p => p.ServiceItem)
                .FirstOrDefault(p => p.Id == serviceDto.Id);
            if (service != null)
            {
                service.Title = serviceDto.Title;
                service.Description = serviceDto.Description;
                service.UpdateTime = DateTime.Now;

                foreach (var item in serviceDto.ServiceItems)
                {
                    var existItem = service.ServiceItem.FirstOrDefault(a => a.Id == item.Id);
                    if (existItem != null)
                    {
                        existItem.Description = item.Description;
                        existItem.Icon = item.Icon;
                        existItem.Title = item.Title;
                        existItem.UpdateTime = DateTime.Now;
                    }
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "با موفقیت ذخیره شد",
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "رکوردی یافت نشد.",
                };
            }
        }

        public ResultDto ExecuteDelete(long id)
        {
            var service = _context.Services
                .Include(p => p.ServiceItem)
                .FirstOrDefault(p => p.Id == id);
            if (service == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            service.RemoveTime = DateTime.Now;
            service.IsRemoved = true;
            foreach (var item in service.ServiceItem)
            {
                item.RemoveTime = DateTime.Now;
                item.IsRemoved = true;
            }
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
    public class ServiceDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ServiceItemDto> ServiceItems { get; set; }
    }

    public class ServiceItemDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
