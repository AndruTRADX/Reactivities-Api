using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Command.Create;

public class CreateActivityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateActivityCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        unitOfWork.Repository<Activity>().AddEntity(request.Activity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(request.Activity.Id);
    }
}
