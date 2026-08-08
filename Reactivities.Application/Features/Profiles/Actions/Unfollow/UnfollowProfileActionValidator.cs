using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Actions.Unfollow;

public class UnfollowProfileActionValidator : AbstractValidator<UnfollowProfileAction>
{
    public UnfollowProfileActionValidator()
    {
        RuleFor(x => x.TargetUserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("TargetUserId is Required.")
            .Length(36)
            .WithMessage("TargetUserId must be 36 characters");
    }
}
