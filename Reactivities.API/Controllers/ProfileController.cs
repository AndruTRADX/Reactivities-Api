using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.Profiles.Actions.AddPhoto;
using Reactivities.Application.Features.Profiles.Actions.Edit;
using Reactivities.Application.Features.Profiles.Actions.Follow;
using Reactivities.Application.Features.Profiles.Actions.RemovePhoto;
using Reactivities.Application.Features.Profiles.Actions.SetMainPhoto;
using Reactivities.Application.Features.Profiles.Actions.Unfollow;
using Reactivities.Application.Features.Profiles.Queries.Get;
using Reactivities.Application.Features.Profiles.Queries.GetPagedFollowers;
using Reactivities.Application.Features.Profiles.Queries.GetPagedFollowing;
using Reactivities.Application.Features.Profiles.Queries.GetPagedPhotos;
using Reactivities.Application.Models.Request.Photos;
using Reactivities.Application.Models.Request.Profiles;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Photos;
using Reactivities.Application.Models.Response.Profiles;

namespace Reactivities.API.Controllers
{
    public class ProfileController : BaseApiController
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

        [HttpPut("edit-profile")]
        public async Task<ActionResult<ApiResponse<UserProfileResponse>>> EditProfile(EditProfileRequest request)
        {
            return await mediator.Send(new EditProfileProfileAction { Profile = request });
        }

        [HttpPost("{userId}/follow")]
        public async Task<ActionResult<ApiResponse<Unit>>> Follow(string userId)
        {
            return await mediator.Send(new FollowProfileAction { TargetUserId = userId });
        }

        [HttpDelete("{userId}/follow")]
        public async Task<ActionResult<ApiResponse<Unit>>> Unfollow(string userId)
        {
            return await mediator.Send(new UnfollowProfileAction { TargetUserId = userId });
        }

        [HttpGet("{userId}/followers")]
        public async Task<ActionResult<ApiResponse<PagedResponse<UserProfileResponse>>>> GetPagedFollowers(string userId, [FromQuery] GetPagedFollowersQuery query)
        {
            query.UserId = userId;
            return await mediator.Send(query);
        }

        [HttpGet("{userId}/following")]
        public async Task<ActionResult<ApiResponse<PagedResponse<UserProfileResponse>>>> GetPagedFollowing(string userId, [FromQuery] GetPagedFollowingQuery query)
        {
            query.UserId = userId;
            return await mediator.Send(query);
        }
    }
}
