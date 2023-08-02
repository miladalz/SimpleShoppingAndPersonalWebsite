using MyWeb.Application.Services.Users.Commands.EditUser;
using MyWeb.Application.Services.Users.Commands.RegisterUser;
using MyWeb.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class UsersController : Controller
    {
        private readonly IUserFacad _userFacad;
        public UsersController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        public IActionResult Index(string serchkey, int page = 1, int pageSize = 10)
        {
            return View(_userFacad.GetUsersService.Execute(new RequestGetUserDto
            {
                Page = page,
                SearchKey = serchkey,
                PageSize = pageSize
            }).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_userFacad.GetRolesService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
        {
            var result = _userFacad.RegisterUserService.Execute(new RequestRegisterUserDto
            {
                Email = Email,
                FullName = FullName,
                roles = new List<RolesInRegisterUserDto>()
                   {
                        new RolesInRegisterUserDto
                        {
                             Id= RoleId
                        }
                   },
                Password = Password,
                RePasword = RePassword,
            });
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_userFacad.RemoveUserService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult UserSatusChange(long UserId)
        {
            return Json(_userFacad.UserSatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(RequestEditUserDto ss)
        {
            return Json(_userFacad.EditUserService.Execute(ss));
        }
    }
}
