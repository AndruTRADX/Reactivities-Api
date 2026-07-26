using FluentValidation;

namespace Reactivities.Application.Features.Profiles.queries.GetPagedPhotos;

public class GetPagedProfilePhotosQueryValidator : AbstractValidator<GetPagedProfilePhotosQuery>
{
    public GetPagedProfilePhotosQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is Required.");
    }
}
