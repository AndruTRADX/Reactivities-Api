using System;
using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Actions.SetMainPhoto;

public class SetMainPhotoProfileActionValidator : AbstractValidator<SetMainPhotoProfileAction>
{
    public SetMainPhotoProfileActionValidator()
    {
        RuleFor(x => x.PhotoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhotoId is Required.")
            .Length(36)
            .WithMessage("PhotoId must be 36 characters");
    }
}
