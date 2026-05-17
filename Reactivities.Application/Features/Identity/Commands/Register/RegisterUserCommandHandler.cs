using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Identity.Commands.Register;

public class RegisterUserCommandHandler(IAuthService authService) : IRequestHandler<RegisterUserCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = await authService.RegisterUserAsync(request.Account);

        return new ApiResponse<Guid>(response.UserId);
    }
}
