using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Actions.Follow;

public class FollowProfileActionValidator : AbstractValidator<FollowProfileAction>
{
    public FollowProfileActionValidator()
    {
        RuleFor(x => x.TargetUserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("TargetUserId is Required.")
            .Length(36)
            .WithMessage("TargetUserId must be 36 characters");
    }
}
