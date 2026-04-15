using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Queries.GetById;

public class GetActivitiesByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetActivitiesByIdQuery, ApiResponse<ActivityResponse>>
{
    public async Task<ApiResponse<ActivityResponse>> Handle(GetActivitiesByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Activity>().GetFirstAsync(x => x.Id == request.Id)
            ?? throw new NotFoundException(nameof(Activity), request.Id);

        return new ApiResponse<ActivityResponse>(mapper.Map<ActivityResponse>(response));
    }
}
