using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;
using MyWeb.Application.Services.Users.Commands.EditUser;
using MyWeb.Application.Services.Users.Commands.RegisterUser;
using MyWeb.Application.Services.Users.Commands.RemoveUser;
using MyWeb.Application.Services.Users.Commands.UserLogin;
using MyWeb.Application.Services.Users.Commands.UserSatusChange;
using MyWeb.Application.Services.Users.Queries.GetRoles;
using MyWeb.Application.Services.Users.Queries.GetUsers;

namespace MyWeb.Application.Services.Users.FacadPattern
{
    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        public UserFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IEditUserService _editUserService;
        public IEditUserService EditUserService
        {
            get
            {
                return _editUserService ??= new EditUserService(_context);
            }
        }

        private IRegisterUserService _registerUserService;
        public IRegisterUserService RegisterUserService
        {
            get
            {
                return _registerUserService ??= new RegisterUserService(_context);
            }
        }

        private IRemoveUserService _removeUserService;
        public IRemoveUserService RemoveUserService
        {
            get
            {
                return _removeUserService ??= new RemoveUserService(_context);
            }
        }

        private IUserLoginService _userLoginService;
        public IUserLoginService UserLoginService
        {
            get
            {
                return _userLoginService ??= new UserLoginService(_context);
            }
        }

        private IUserSatusChangeService _userSatusChangeService;
        public IUserSatusChangeService UserSatusChangeService
        {
            get
            {
                return _userSatusChangeService ??= new UserSatusChangeService(_context);
            }
        }

        private IGetRolesService _getRolesService;
        public IGetRolesService GetRolesService
        {
            get
            {
                return _getRolesService ??= new GetRolesService(_context);
            }
        }

        private IGetUsersService _getUsersService;
        public IGetUsersService GetUsersService
        {
            get
            {
                return _getUsersService ??= new GetUsersService(_context);
            }
        }
    }
}
