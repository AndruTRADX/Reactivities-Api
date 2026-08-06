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
            .MaximumLength(256)
            .WithMessage("Email must not exceed 256 characters")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Must be a valid email");

        RuleFor(x => x.Account.Password)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Password is required and must have at least 3 characters")
            .MaximumLength(512)
            .WithMessage("Password must not exceed 512 characters");

        RuleFor(x => x.Account.DisplayName)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("DisplayName is required and must have at least 3 characters")
            .MaximumLength(50)
            .WithMessage("DisplayName must not exceed 50 characters");

        RuleFor(x => x.Account.Biography)
            .MaximumLength(1000)
            .WithMessage("Biography must not exceed 1000 characters");

        RuleFor(x => x.Account.ImageUrl)
            .MaximumLength(500)
            .WithMessage("ImageUrl must not exceed 500 characters");
    }
}
