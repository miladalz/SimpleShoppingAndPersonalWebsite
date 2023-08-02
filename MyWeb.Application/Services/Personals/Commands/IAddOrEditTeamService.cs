using Microsoft.EntityFrameworkCore;
using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Personal;

namespace MyWeb.Application.Services.Personals.Commands
{
    public interface IAddOrEditTeamService
    {
        ResultDto ExecuteAdd(TeamDto teamDto);
        ResultDto ExecuteEdit(TeamDto teamDto);
        ResultDto ExecuteDelete(long id);
    }
    public class AddOrEditTeamService : IAddOrEditTeamService
    {
        private readonly IDataBaseContext _context;
        public AddOrEditTeamService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto ExecuteAdd(TeamDto teamDto)
        {
            var team = new Team()
            {
                Title = teamDto.Title,
                Description = teamDto.Description
            };
            _context.Teams.Add(team);

            List<TeamMember> teamMembers = new();
            foreach (var item in teamDto.TeamMemberItems)
            {
                teamMembers.Add(new TeamMember
                {
                    Twitter = item.Twitter,
                    Description = item.Description,
                    Facebook = item.Facebook,
                    GooglePlus = item.GooglePlus,
                    Image = item.Image,
                    Jobtitle = item.Jobtitle,
                    Linkedin = item.Linkedin,
                    Name = item.Name,
                    Team = team
                });
            }
            _context.TeamMembers.AddRange(teamMembers);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت ذخیره شد",
            };
        }

        public ResultDto ExecuteEdit(TeamDto teamDto)
        {
            var team = _context.Teams
                .Include(p => p.TeamMemberItems)
                .FirstOrDefault(p => p.Id == teamDto.Id);
            if (team != null)
            {
                team.Title = teamDto.Title;
                team.Description = teamDto.Description;
                team.UpdateTime = DateTime.Now;

                foreach (var item in teamDto.TeamMemberItems)
                {
                    var existItem = team.TeamMemberItems.FirstOrDefault(a => a.Id == item.Id);
                    if (existItem != null)
                    {
                        existItem.Twitter = item.Twitter;
                        existItem.Description = item.Description;
                        existItem.Facebook = item.Facebook;
                        existItem.GooglePlus = item.GooglePlus;
                        existItem.Image = item.Image;
                        existItem.Jobtitle = item.Jobtitle;
                        existItem.Linkedin = item.Linkedin;
                        existItem.Name = item.Name;
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
            var team = _context.Teams
                .Include(p => p.TeamMemberItems)
                .FirstOrDefault(p => p.Id == id);
            if (team == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "اطلاعات یافت نشد"
                };
            }
            team.RemoveTime = DateTime.Now;
            team.IsRemoved = true;
            foreach (var item in team.TeamMemberItems)
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
    public class TeamDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TeamMemberDto> TeamMemberItems { get; set; }
    }

    public class TeamMemberDto
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
