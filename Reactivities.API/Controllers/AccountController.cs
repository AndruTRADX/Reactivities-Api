using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.Identity.Commands.Register;
using Reactivities.Application.Features.Identity.Commands.SignOut;
using Reactivities.Application.Models.Request.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.Controllers;

public class AccountController : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<Guid>>> RegisterUser(RegisterUserRequest account)
    {
        return await mediator.Send(new RegisterUserCommand { Account = account });
    }

    [HttpPost("signout")]
    public new async Task<ActionResult<ApiResponse<Unit>>> SignOut()
    {
        return await mediator.Send(new SignOutUserCommand());
    }
}
