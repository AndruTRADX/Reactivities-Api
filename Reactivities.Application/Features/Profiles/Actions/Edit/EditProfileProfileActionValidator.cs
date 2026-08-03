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
            .WithMessage("DisplayName is Required.");
    }
}
