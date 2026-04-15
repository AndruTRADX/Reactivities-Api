using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteActivityCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Activity>().GetFirstAsync(p => p.Id == request.Id)
            ?? throw new NotFoundException(nameof(Activity), request.Id);

        unitOfWork.Repository<Activity>().DeleteEntity(response);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}
