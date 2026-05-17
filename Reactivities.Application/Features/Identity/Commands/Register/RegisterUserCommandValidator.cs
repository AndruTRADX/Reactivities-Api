using FluentValidation;

namespace Reactivities.Application.Features.Identity.Commands.Register;

public class RegisterAccountCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterAccountCommandValidator()
    {
        RuleFor(x => x.Account.Email)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Email is required and must have at least 3 characters")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Must be a valid email");

        RuleFor(x => x.Account.Password)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Password is required and must have at least 3 characters");

        RuleFor(x => x.Account.DisplayName)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("DisplayName is required and must have at least 3 characters");
    }
}
