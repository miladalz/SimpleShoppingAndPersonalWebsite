﻿using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Common.Dto;
using MyWeb.Domain.Entities.Finances;

namespace MyWeb.Application.Services.Finances.Commands.AddRequestPay
{
    public class AddRequestPayService : IAddRequestPayService
    {
        private readonly IDataBaseContext _context;
        public AddRequestPayService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId)
        {
            var user = _context.Users.Find(UserId);
            RequestPay requestPay = new RequestPay()
            {
                Amount = Amount,
                Guid = Guid.NewGuid(),
                IsPay = false,
                User = user,

            };
            _context.RequestPays.Add(requestPay);
            _context.SaveChanges();

            return new ResultDto<ResultRequestPayDto>()
            {
                Data = new ResultRequestPayDto
                {
                    guid = requestPay.Guid,
                    Amount=requestPay.Amount,
                    Email=user.Email,
                    RequestPayId=requestPay.Id,
                },
                IsSuccess = true,
            };
        }
    }
}
