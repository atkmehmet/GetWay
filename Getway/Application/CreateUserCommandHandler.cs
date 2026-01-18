using FluentValidation;
using Getway.Interface;

namespace Getway.Application
{
    public class CreateUserCommandHandler
    {
        private readonly IValidator<CreateUserRequestValidator> _validator;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(
            IValidator<CreateUserRequestValidator> validator,
            IUserRepository userRepository
            ) {
                
            _validator = validator;
            _userRepository = userRepository;
        }


    }
}
