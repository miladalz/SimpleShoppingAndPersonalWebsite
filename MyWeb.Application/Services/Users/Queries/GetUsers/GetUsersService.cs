using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<UserPage> Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(request.SearchKey) || p.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var usersList= users.ToPaged(request.Page, request.PageSize, out rowsCount).Select(p => new GetUsersDto
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
                IsActive=p.IsActive
            }).ToList();            

            return new ResultDto<UserPage>()
            {
                Data = new UserPage()
                {
                    Users = usersList,
                    CurrentPage = request.Page,
                    PageSize = request.PageSize,
                    RowCount = rowsCount
                },
                IsSuccess = true,
                Message = "",
            };
        }
    }
}
