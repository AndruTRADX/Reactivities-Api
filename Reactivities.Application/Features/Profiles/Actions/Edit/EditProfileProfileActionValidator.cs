using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Actions.Edit;

public class EditProfileProfileActionValidator : AbstractValidator<EditProfileProfileAction>
{
    public EditProfileProfileActionValidator()
    {
        RuleFor(x => x.Profile).NotNull();

        RuleFor(x => x.Profile.DisplayName)
            .NotNull()
            .NotEmpty()
            .WithMessage("DisplayName is Required.")
            .MaximumLength(50)
            .WithMessage("DisplayName must not exceed 50 characters");

        RuleFor(x => x.Profile.Biography)
            .MaximumLength(1000)
            .WithMessage("Biography must not exceed 1000 characters");
    }
}
