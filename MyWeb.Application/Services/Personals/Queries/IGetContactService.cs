using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetContactService
    {
        ResultDto<List<ContactList>> Execute();
        ResultDto<ContactPage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        ContactList ExecuteForEdit(long id);
    }
    public class GetContactService : IGetContactService
    {
        private readonly IDataBaseContext _context;
        public GetContactService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ContactList>> Execute()
        {
            var contacts = _context.Contacts
                .Include(p => p.ContactItems)
                .ToList()
                .Select(p => new ContactList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ContactItems = p.ContactItems.ToList()
                    .Select(c => new Contact
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Type = c.Type
                    }).ToList()
                }).ToList();
            return new ResultDto<List<ContactList>>()
            {
                Data = contacts,
                IsSuccess = true,
            };
        }

        public ResultDto<ContactPage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Contacts
                .Include(p => p.ContactItems)
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new ContactList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ContactItems = p.ContactItems.ToList()
                    .Select(c => new Contact
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Type = c.Type
                    }).ToList()
                }).ToList();
            return new ResultDto<ContactPage>()
            {
                Data = new ContactPage()
                {
                    Contacts = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public ContactList ExecuteForEdit(long id)
        {
            var contact = _context.Contacts
                .Include(p => p.ContactItems)
                .Where(p => p.Id == id)
                .Select(p => new ContactList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    ContactItems = p.ContactItems.ToList()
                    .Select(c => new Contact
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Icon = c.Icon,
                        Type = c.Type
                    }).ToList()
                }).FirstOrDefault();
            return contact;
        }
    }
    public class ContactPage : ResultPage
    {
        public List<ContactList> Contacts { get; set; }
    }
    public class ContactList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Contact> ContactItems { get; set; }
    }
    public class Contact
    {
        public long Id { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
