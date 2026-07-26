using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Queries.GetPagedPhotos;

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
