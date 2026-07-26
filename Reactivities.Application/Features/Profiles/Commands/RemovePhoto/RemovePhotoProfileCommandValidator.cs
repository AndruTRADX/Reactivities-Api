using System;
using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Commands.RemovePhoto;

public class RemovePhotoProfileCommandValidator : AbstractValidator<RemovePhotoProfileCommand>
{
    public RemovePhotoProfileCommandValidator()
    {
        RuleFor(x => x.PhotoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhotoId is Required.");
    }
}
