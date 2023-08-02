using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditContactService
    {
        ResultDto ExecuteAdd(ContactDto contactDto);
        ResultDto ExecuteEdit(ContactDto contactDto);
        ResultDto ExecuteDelete(long id);
    }

    public class AddOrEditContactService : IAddOrEditContactService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditContactService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(ContactDto contactDto)
        {
            var contact = new Contact()
            {
                Title = contactDto.Title,
                Description = contactDto.Description
            };
            _context.Contacts.Add(contact);

            List<ContactItem> contactItems = new();
            foreach (var item in contactDto.ContactItems)
            {
                contactItems.Add(new ContactItem()
                {
                    Description = item.Description,
                    Icon = item.Icon,
                    Type = item.Type,
                    Contact = contact
                });
            }
            _context.ContactItems.AddRange(contactItems);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }
        public ResultDto ExecuteEdit(ContactDto contactDto)
        {
            var contact = _context.Contacts
                .Include(p => p.ContactItems)
                .FirstOrDefault(p => p.Id == contactDto.Id);
            if (contact != null)
            {
                contact.Title = contactDto.Title;
                contact.Description = contactDto.Description;
                contact.UpdateTime = DateTime.Now;
                foreach (var item in contactDto.ContactItems)
                {
                    var existItem = contact.ContactItems.FirstOrDefault(a => a.Id == item.Id);
                    if (existItem != null)
                    {
                        existItem.Description = item.Description;
                        existItem.Icon = item.Icon;
                        existItem.Type = item.Type;
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
            var contact = _context.Contacts
                .Include(p => p.ContactItems)
                .FirstOrDefault(p => p.Id == id);
            if (contact == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            contact.RemoveTime = DateTime.Now;
            contact.IsRemoved = true;
            foreach (var item in contact.ContactItems)
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
    public class ContactDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ContactItemDto> ContactItems { get; set; }
    }
    public class ContactItemDto
    {
        public long Id { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
