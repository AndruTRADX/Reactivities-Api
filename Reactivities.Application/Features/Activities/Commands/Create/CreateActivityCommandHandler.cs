using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Commands.Create;

public class CreateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateActivityCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var data = mapper.Map<Activity>(request.Activity);
        unitOfWork.Repository<Activity>().AddEntity(data);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(data.Id);
    }
}
