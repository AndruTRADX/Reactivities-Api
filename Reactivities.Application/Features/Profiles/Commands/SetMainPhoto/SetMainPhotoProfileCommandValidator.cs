using System;
using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Commands.SetMainPhoto;

public class SetMainPhotoProfileCommandValidator : AbstractValidator<SetMainPhotoProfileCommand>
{
    public SetMainPhotoProfileCommandValidator()
    {
        RuleFor(x => x.PhotoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhotoId is Required.");
    }
}
