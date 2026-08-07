using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.ActivityComments.Commands.Create;

public class AddActivityCommentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddActivityCommentCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(AddActivityCommentCommand request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.Repository<Activity>().GetFirstAsync(predicate: x => x.Id == request.ActivityId, includeStrings: ["Comments"], enabledTracking: true)
            ?? throw new NotFoundException(nameof(Activity), request.ActivityId);



        return new ApiResponse<Unit>();
    }
}
