using MediatR;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Photos;
using Reactivities.Application.Specification.Photos;

namespace Reactivities.Application.Features.Profiles.queries.GetPagedPhotos;

public class GetPagedProfilePhotosQuery : PhotoSpecificationParams, IRequest<ApiResponse<PagedResponse<PhotoResponse>>>
{
    
}
