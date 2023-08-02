using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Personals.Queries
{
    public interface IGetTeamService
    {
        ResultDto<List<TeamList>> Execute();
        ResultDto<TeamPage> ExecuteForAdmin(int Page = 1, int PageSize = 10);
        TeamList ExecuteForEdit(long id);
    }

    public class GetTeamService : IGetTeamService
    {
        private readonly IDataBaseContext _context;
        public GetTeamService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<TeamList>> Execute()
        {
            var teams = _context.Teams
                .Include(p => p.TeamMemberItems)
                .ToList()
                .Select(p => new TeamList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    TeamMemberItems = p.TeamMemberItems.ToList()
                    .Select(c => new TeamMember
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Facebook = c.Facebook,
                        GooglePlus = c.GooglePlus,
                        Image = c.Image,
                        Jobtitle = c.Jobtitle,
                        Linkedin = c.Linkedin,
                        Name = c.Name,
                        Twitter = c.Twitter
                    }).ToList()
                }).ToList();
            return new ResultDto<List<TeamList>>()
            {
                Data = teams,
                IsSuccess = true,
            };
        }
        public ResultDto<TeamPage> ExecuteForAdmin(int Page = 1, int PageSize = 10)
        {
            int rowCount = 0;
            var items = _context.Teams
                .Include(p => p.TeamMemberItems)
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new TeamList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    TeamMemberItems = p.TeamMemberItems.ToList()
                    .Select(c => new TeamMember
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Facebook = c.Facebook,
                        GooglePlus = c.GooglePlus,
                        Image = c.Image,
                        Jobtitle = c.Jobtitle,
                        Linkedin = c.Linkedin,
                        Name = c.Name,
                        Twitter = c.Twitter
                    }).ToList()
                }).ToList();
            return new ResultDto<TeamPage>()
            {
                Data = new TeamPage()
                {
                    Teams = items,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = "",
            };
        }

        public TeamList ExecuteForEdit(long id)
        {
            var teams = _context.Teams
                .Include(p => p.TeamMemberItems)
                .Where(p => p.Id == id)
                .Select(p => new TeamList
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    TeamMemberItems = p.TeamMemberItems.ToList()
                    .Select(c => new TeamMember
                    {
                        Description = c.Description,
                        Id = c.Id,
                        Facebook = c.Facebook,
                        GooglePlus = c.GooglePlus,
                        Image = c.Image,
                        Jobtitle = c.Jobtitle,
                        Linkedin = c.Linkedin,
                        Name = c.Name,
                        Twitter = c.Twitter
                    }).ToList()
                }).FirstOrDefault();
            return teams;
        }
    }
    public class TeamPage : ResultPage
    {
        public List<TeamList> Teams { get; set; }
    }
    public class TeamList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TeamMember> TeamMemberItems { get; set; }
    }

    public class TeamMember
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Jobtitle { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string GooglePlus { get; set; }
        public string Twitter { get; set; }
    }
}
