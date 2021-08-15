using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codelux.NetCore.Common.Extensions;
using Codelux.NetCore.Executors;
using Codelux.NetCore.Mappers;
using Codelux.NetCore.Utilities.Crypto;
using Example.Users.ManagementService.Common;
using Example.Users.ManagementService.Common.Models;
using Example.Users.ManagementService.Common.Requests;
using Example.Users.ManagementService.Common.Responses;
using Example.Users.ManagementService.Core.DataAccess;

namespace Example.Users.ManagementService.Executors.RegisterUserExecutor
{
    public class RegisterUserExecutor : ExecutorBase<RegisterUserRequest, RegisterUserResponse>, IRegisterUserExecutor
    {
        private readonly IMapper<RegisterUserRequest, User> _mapper;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IUserRepository _userRepository;

        public RegisterUserExecutor(IMapper<RegisterUserRequest, User> mapper, IPasswordEncryptor passwordEncryptor, IUserRepository userRepository)
        {
            mapper.Guard(nameof(mapper));
            passwordEncryptor.Guard(nameof(passwordEncryptor));
            userRepository.Guard(nameof(userRepository));

            _mapper = mapper;
            _passwordEncryptor = passwordEncryptor;
            _userRepository = userRepository;
        }

        protected override async Task<RegisterUserResponse> OnExecuteAsync(RegisterUserRequest tin, CancellationToken token = new CancellationToken())
        {
            User user = _mapper.Map(tin);

            user.Password = _passwordEncryptor.Encrypt(tin.Password);

            bool result = await _userRepository.CreateUserAsync(user, token).ConfigureAwait(false);

            if (!result) throw ServiceErrors.CannotCreateUserException;

            return new RegisterUserResponse()
            {
                UserId = user.Id
            };
        }
    }
}
