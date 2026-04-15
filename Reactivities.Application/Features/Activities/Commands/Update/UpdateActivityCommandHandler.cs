using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Commands.Update;

public class UpdateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateActivityCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Activity>().GetFirstAsync(x => x.Id == request.Activity.Id)
            ?? throw new NotFoundException(nameof(Activity), request.Activity.Id);

        var data = mapper.Map(request.Activity, response);

        unitOfWork.Repository<Activity>().UpdateEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}
