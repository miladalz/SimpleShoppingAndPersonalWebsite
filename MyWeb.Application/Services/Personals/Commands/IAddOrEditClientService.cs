using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditClientService
    {
        ResultDto ExecuteAdd(ClientDto clientDto);
        ResultDto ExecuteEdit(ClientDto clientDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditClientService : IAddOrEditClientService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditClientService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(ClientDto clientDto)
        {
            _context.Clinets.Add(new Clinet()
            {
                Title = clientDto.Title,
                Description = clientDto.Description,
                Link = clientDto.Link,
                Image = clientDto.Image,
                Comment = clientDto.Comment,
                TitleImage = clientDto.TitleImage
            });
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }
        public ResultDto ExecuteEdit(ClientDto clientDto)
        {
            var client = _context.Clinets.FirstOrDefault(a => a.Id == clientDto.Id);
            if (client != null)
            {
                client.Title = clientDto.Title;
                client.Description = clientDto.Description;
                client.Link = clientDto.Link;
                client.Image = clientDto.Image;
                client.Comment = clientDto.Comment;
                client.TitleImage = clientDto.TitleImage;
            }
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }
        public ResultDto ExecuteDelete(long id)
        {
            var client = _context.Clinets               
                .FirstOrDefault(p => p.Id == id);
            if (client == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            client.RemoveTime = DateTime.Now;
            client.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
    public class ClientDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string Comment { get; set; }
        public string TitleImage { get; set; }
    }
}
