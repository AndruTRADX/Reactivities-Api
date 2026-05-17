using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Identity.Commands.SignOut;

public class SignOutUserCommandHandler(IAuthService authService) : IRequestHandler<SignOutUserCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(SignOutUserCommand request, CancellationToken cancellationToken)
    {
        await authService.SignOutAsync();

        return new ApiResponse<Unit>();
    }
}
