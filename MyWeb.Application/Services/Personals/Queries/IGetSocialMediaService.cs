using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetSocialMediaService
    {
        ResultDto<List<SocialMediaList>> Execute();
        SocialMediaList ExecuteForEdit(long id);
    }
    public class GetSocialMediaService : IGetSocialMediaService
    {
        private readonly IDataBaseContext _context;
        public GetSocialMediaService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SocialMediaList>> Execute()
        {
            var medias = _context.SocialMedias
                .OrderByDescending(p => p.Id)
                .Select(p => new SocialMediaList
                {
                    Id = p.Id,
                    Type = p.Type,
                    Link = p.Link,
                    Icon = p.Icon
                }).ToList();
            return new ResultDto<List<SocialMediaList>>()
            {
                Data = medias,
                IsSuccess = true,
            };
        }

        public SocialMediaList ExecuteForEdit(long id)
        {
            var medias = _context.SocialMedias
                .Where(p => p.Id == id)
                .Select(p => new SocialMediaList
                {
                    Id = p.Id,
                    Type = p.Type,
                    Link = p.Link,
                    Icon = p.Icon
                }).FirstOrDefault();
            return medias;
        }
    }
    public class SocialMediaList
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
    }
}
