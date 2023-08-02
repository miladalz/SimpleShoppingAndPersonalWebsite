using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditMessageService
    {
        ResultDto ExecuteAdd(MessageDto messageDto);
        ResultDto ExecuteEdit(MessageDto messageDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditMessageService: IAddOrEditMessageService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditMessageService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto ExecuteAdd(MessageDto messageDto)
        {
            _context.Messages.Add(new Message()
            {
                Name = messageDto.Name,
                Email = messageDto.Email,
                Text = messageDto.Text
            });
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "پیام با موفقیت ارسال شد.",
            };
        }

        public ResultDto ExecuteEdit(MessageDto messageDto)
        {
            var message = _context.Messages
                .FirstOrDefault(p => p.Id == messageDto.Id);
            if (messageDto != null)
            {
                message.Name = messageDto.Name;
                message.Email = messageDto.Email;
                message.Text = messageDto.Text;
                message.UpdateTime = DateTime.Now;

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
            var message = _context.Messages
                .FirstOrDefault(p => p.Id == id);
            if (message == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            message.RemoveTime = DateTime.Now;
            message.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
    public class MessageDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }
}
