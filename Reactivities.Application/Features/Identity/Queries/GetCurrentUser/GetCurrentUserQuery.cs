using MediatR;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Identity;

namespace Reactivities.Application.Features.Identity.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<ApiResponse<UserResponse?>>
{
    
}
