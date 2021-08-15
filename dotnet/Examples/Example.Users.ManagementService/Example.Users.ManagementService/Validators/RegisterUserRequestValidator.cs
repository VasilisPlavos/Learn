using Example.Users.ManagementService.Common.Requests;
using ServiceStack.FluentValidation;

namespace Example.Users.ManagementService.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .Length(5, 50).WithMessage("Username must be 5 to 50 characters long.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail cannot be empty.")
                .EmailAddress().WithMessage("Invalid e-mail address.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty");
        }
    }
}
