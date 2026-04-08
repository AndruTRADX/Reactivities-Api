using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Specification.Activities;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Query.GetPaged;

public class GetPagedActivitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetPagedActivitiesQuery, ApiResponse<PagedResponse<ActivityResponse>>>
{
    public async Task<ApiResponse<PagedResponse<ActivityResponse>>> Handle(GetPagedActivitiesQuery request, CancellationToken cancellationToken)
    {
        var spec = new ActivitySpecification(request);
        var response = await unitOfWork.Repository<Activity>().GetAllWithSpec(spec);

        var specCount = new ActivityCountSpecification(request);
        var totalCount = await unitOfWork.Repository<Activity>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var data = mapper.Map<IReadOnlyList<Activity>, IReadOnlyList<ActivityResponse>>(response);

        return new ApiResponse<PagedResponse<ActivityResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
