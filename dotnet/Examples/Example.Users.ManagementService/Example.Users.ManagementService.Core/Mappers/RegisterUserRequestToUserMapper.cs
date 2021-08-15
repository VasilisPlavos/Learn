using System;
using Codelux.NetCore.Common.Extensions;
using Codelux.NetCore.Mappers;
using Codelux.NetCore.Utilities;
using Example.Users.ManagementService.Common.Models;
using Example.Users.ManagementService.Common.Requests;

namespace Example.Users.ManagementService.Core.Mappers
{
    public class RegisterUserRequestToUserMapper : MapperBase<RegisterUserRequest, User>
    {
        private readonly IClockService _clockService;

        public RegisterUserRequestToUserMapper(IClockService clockService)
        {
            clockService.Guard(nameof(clockService));
            _clockService = clockService;
        }


        public override User Map(RegisterUserRequest model)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedAt = new DateTimeOffset(_clockService.Now()).ToUnixTimeMilliseconds()
            };
        }
    }
}
