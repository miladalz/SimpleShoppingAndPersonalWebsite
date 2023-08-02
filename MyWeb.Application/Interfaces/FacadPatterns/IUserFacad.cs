using MyWeb.Application.Services.Users.Commands.EditUser;
using MyWeb.Application.Services.Users.Commands.RegisterUser;
using MyWeb.Application.Services.Users.Commands.RemoveUser;
using MyWeb.Application.Services.Users.Commands.UserLogin;
using MyWeb.Application.Services.Users.Commands.UserSatusChange;
using MyWeb.Application.Services.Users.Queries.GetRoles;
using MyWeb.Application.Services.Users.Queries.GetUsers;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface IUserFacad
    {
        IEditUserService EditUserService { get; }
        IRegisterUserService RegisterUserService { get; }
        IRemoveUserService RemoveUserService { get; }
        IUserLoginService UserLoginService { get; }
        IUserSatusChangeService UserSatusChangeService { get; }
        IGetRolesService GetRolesService { get; }
        IGetUsersService GetUsersService { get; }
    }
}
