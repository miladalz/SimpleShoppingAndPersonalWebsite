﻿using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;

namespace MyWeb.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDataBaseContext _context;

        public GetRolesService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RolesDto>> Execute()
        {
            var roles = _context.Roles.ToList().Select(p => new RolesDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = "",
            };
        }
    }
}
