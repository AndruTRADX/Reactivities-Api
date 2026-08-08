using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.ActivityComments;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.ActivityComments.Commands.Create;

public class AddActivityCommentCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper) : IRequestHandler<AddActivityCommentCommand, ApiResponse<ActivityCommentResponse>>
{
    public async Task<ApiResponse<ActivityCommentResponse>> Handle(AddActivityCommentCommand request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.Repository<Activity>().GetFirstAsync(predicate: x => x.Id == request.ActivityId, includeStrings: ["Comments"], enabledTracking: true)
            ?? throw new NotFoundException(nameof(Activity), request.ActivityId);

        var userId = userAccessor.GetUserId();
        var comment = data.AddComment(userId, request.Request.Body);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var savedComment = await unitOfWork.Repository<ActivityComment>().GetFirstAsync(predicate: x => x.Id == comment.Id, includeStrings: ["User"], enabledTracking: false)
            ?? throw new NotFoundException(nameof(ActivityComment), comment.Id);

        return new ApiResponse<ActivityCommentResponse>(mapper.Map<ActivityCommentResponse>(savedComment));
    }
}
