using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Users.Queries.GetRoles
{
    public interface IGetRolesService
    {
        ResultDto<List<RolesDto>> Execute();
    }

    public class RolesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
