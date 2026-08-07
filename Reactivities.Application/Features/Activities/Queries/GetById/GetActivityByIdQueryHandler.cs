using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Queries.GetById;

public class GetActivityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetActivityByIdQuery, ApiResponse<ActivityResponse>>
{
    public async Task<ApiResponse<ActivityResponse>> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork
            .Repository<Activity>()
            .GetFirstAsync(predicate: x => x.Id == request.Id, includeStrings: ["Attendees.User",]) 
        ?? throw new NotFoundException(nameof(Activity), request.Id);

        return new ApiResponse<ActivityResponse>(mapper.Map<ActivityResponse>(response));
    }
}
