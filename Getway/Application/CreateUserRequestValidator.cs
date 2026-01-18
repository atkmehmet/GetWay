using FluentValidation;

namespace Getway.Application
{
    public class CreateUserRequestValidator:AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator() {

            RuleFor(x=> x.Email)
                .NotEmpty().WithMessage("Email bos olmaz")
                .EmailAddress().WithMessage("Email formati gecersiz");      
                }
    }
}
