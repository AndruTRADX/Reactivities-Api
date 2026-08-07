using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.ActivityComments.Commands.Delete;

public class DeleteActivityCommentCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<DeleteActivityCommentCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeleteActivityCommentCommand request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.Repository<Activity>().GetFirstAsync(predicate: x => x.Id == request.ActivityId, includeStrings: ["Comments"], enabledTracking: true)
            ?? throw new NotFoundException(nameof(Activity), request.ActivityId);

        var userId = userAccessor.GetUserId();
        data.RemoveComment(request.CommentId, userId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}
