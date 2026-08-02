using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.Profiles.Actions.AddPhoto;
using Reactivities.Application.Features.Profiles.Actions.RemovePhoto;
using Reactivities.Application.Features.Profiles.Actions.SetMainPhoto;
using Reactivities.Application.Features.Profiles.Queries.Get;
using Reactivities.Application.Features.Profiles.Queries.GetPagedPhotos;
using Reactivities.Application.Models.Request.Photos;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Photos;
using Reactivities.Application.Models.Response.Profile;

namespace Reactivities.API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> GetProfile(string userId)
        {
            return await mediator.Send(new GetProfileQuery { UserId = userId });
        }

        [HttpGet("{userId}/photos")]
        public async Task<ActionResult<ApiResponse<PagedResponse<PhotoResponse>>>> GetPagedProfilePhotos(string userId)
        {
            return await mediator.Send(new GetPagedProfilePhotosQuery { UserId = userId });
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<ApiResponse<PhotoResponse>>> AddPhoto([FromForm] AddPhotoRequest request)
        {
            return await mediator.Send(new AddPhotoProfileAction { Photo = request });
        }

        [HttpDelete("{photoId}/photos")]
        public async Task<ActionResult<ApiResponse<Unit>>> DeletePhoto(string photoId)
        {
            return await mediator.Send(new RemovePhotoProfileAction { PhotoId = photoId });
        }

        [HttpPut("{photoId}/set-main-photo")]
        public async Task<ActionResult<ApiResponse<Unit>>> SetMainPhoto(string photoId)
        {
            return await mediator.Send(new SetMainPhotoProfileAction { PhotoId = photoId });
        }
    }
}
