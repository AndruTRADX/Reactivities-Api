using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Reactivities.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator service is unavailable");
    }
}
