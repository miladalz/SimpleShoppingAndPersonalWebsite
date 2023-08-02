using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditSocialMediaService
    {
        ResultDto ExecuteAdd(AddSocialMediaDto socialMediaDto);
        ResultDto ExecuteEdit(AddSocialMediaDto socialMediaDto);
        ResultDto ExecuteDelete(long id);
    }

    public class AddOrEditSocialMediaService : IAddOrEditSocialMediaService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditSocialMediaService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(AddSocialMediaDto socialMediaDto)
        {
            _context.SocialMedias.Add(new SocialMedia()
            {
                Type = socialMediaDto.Type,
                Icon = socialMediaDto.Icon,
                Link = socialMediaDto.Link
            });
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(AddSocialMediaDto socialMediaDto)
        {
            var social = _context.SocialMedias
                .FirstOrDefault(p => p.Id == socialMediaDto.Id);
            if (social != null)
            {
                social.Type = socialMediaDto.Type;
                social.Icon = socialMediaDto.Icon;
                social.Link = socialMediaDto.Link;
                social.UpdateTime = DateTime.Now;

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
            var socialMedia = _context.SocialMedias
                .FirstOrDefault(p => p.Id == id);
            if (socialMedia == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            socialMedia.RemoveTime = DateTime.Now;
            socialMedia.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }

    public class AddSocialMediaDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
    }
}
