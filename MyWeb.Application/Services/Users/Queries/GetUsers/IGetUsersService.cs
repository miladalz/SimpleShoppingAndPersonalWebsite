using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultDto<UserPage> Execute(RequestGetUserDto request);
    }

    public class GetUsersDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    public class RequestGetUserDto
    {
        public string SearchKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class UserPage : ResultPage
    {
        public List<GetUsersDto> Users { get; set; }
    }
}
