using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Identity.Commands.SignOut;

public class SignOutUserCommand : IRequest<ApiResponse<Unit>>
{

}
