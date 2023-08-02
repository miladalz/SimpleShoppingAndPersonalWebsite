using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
	public interface IAddOrEditSiteNewsMemberService
	{
        ResultDto ExecuteAdd(SiteNewsMemberDto siteNewsMemberDto);
        ResultDto ExecuteEdit(SiteNewsMemberDto siteNewsMemberDto);
        ResultDto ExecuteDelete(long id);
    }

    public class AddOrEditSiteNewsMemberService : IAddOrEditSiteNewsMemberService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditSiteNewsMemberService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto ExecuteAdd(SiteNewsMemberDto siteNewsMemberDto)
        {
            _context.SiteNewsMembers.Add(new SiteNewsMember()
            {
                Email = siteNewsMemberDto.Email,
            });
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "شما با موفقیت عضو خبرنامه سایت شدید",
            };
        }

        public ResultDto ExecuteEdit(SiteNewsMemberDto siteNewsMemberDto)
        {
            var siteNewsMember = _context.SiteNewsMembers
                .FirstOrDefault(p => p.Id == siteNewsMemberDto.Id);
            if (siteNewsMember != null)
            {
                siteNewsMember.Email = siteNewsMemberDto.Email;
                siteNewsMember.UpdateTime = DateTime.Now;

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
            var siteNewsMember = _context.SiteNewsMembers
                .FirstOrDefault(p => p.Id == id);
            if (siteNewsMember == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            siteNewsMember.RemoveTime = DateTime.Now;
            siteNewsMember.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }

    public class SiteNewsMemberDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
    }
}
