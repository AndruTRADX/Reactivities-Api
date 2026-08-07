using Reactivities.Domain;

namespace Reactivities.Application.Specifications.Photos;

public class PhotoCountSpecification(PhotoSpecificationParams specParams) : BaseSpecification<Photo>(
    x =>
        string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId.Contains(specParams.UserId)
    )
{ }
