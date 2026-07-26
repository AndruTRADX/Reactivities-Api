using Reactivities.Domain;

namespace Reactivities.Application.Specification.Photos;

public class PhotoCountSpecification(PhotoSpecificationParams specParams) : BaseSpecification<Photo>(
    x =>
        string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId.Contains(specParams.UserId)
    )
{ }
