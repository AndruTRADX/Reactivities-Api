using MediatR;
using Reactivities.Application.Models.Request.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Identity.Commands.Register;

public class RegisterUserCommand : IRequest<ApiResponse<Guid>>
{
    public required RegisterUserRequest Account { get; set; }
}
