using System;
using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Actions.RemovePhoto;

public class RemovePhotoProfileActionValidator : AbstractValidator<RemovePhotoProfileAction>
{
    public RemovePhotoProfileActionValidator()
    {
        RuleFor(x => x.PhotoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhotoId is Required.");
    }
}
