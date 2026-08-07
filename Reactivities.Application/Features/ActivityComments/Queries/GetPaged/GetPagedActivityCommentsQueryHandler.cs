using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response.ActivityComments;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Specifications.ActivityComments;
using Reactivities.Domain;

namespace Reactivities.Application.Features.ActivityComments.Queries.GetPaged;

public class GetPagedActivityCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetPagedActivityCommentsQuery, ApiResponse<PagedResponse<ActivityCommentResponse>>>
{
    public async Task<ApiResponse<PagedResponse<ActivityCommentResponse>>> Handle(GetPagedActivityCommentsQuery request, CancellationToken cancellationToken)
    {
        var spec = new ActivityCommentSpecification(request);
        var response = await unitOfWork.Repository<ActivityComment>().GetAllWithSpec(spec);

        var specCount = new ActivityCommentCountSpecification(request);
        var totalCount = await unitOfWork.Repository<ActivityComment>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var data = mapper.Map<IReadOnlyList<ActivityComment>, IReadOnlyList<ActivityCommentResponse>>(response);

        return new ApiResponse<PagedResponse<ActivityCommentResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
