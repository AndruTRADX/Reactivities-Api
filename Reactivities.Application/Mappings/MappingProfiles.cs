using AutoMapper;
using Reactivities.Application.Models.Request.Activities;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.ActivityAttendees;
using Reactivities.Application.Models.Response.ActivityComments;
using Reactivities.Application.Models.Response.Identity;
using Reactivities.Application.Models.Response.Photos;
using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Domain;
using Reactivities.Domain.Identity;

namespace Reactivities.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        string? currentUserId = null;

        CreateMap<CreateActivityRequest, Activity>();
        CreateMap<UpdateActivityRequest, Activity>();

        CreateMap<Activity, ActivityResponse>();
        CreateMap<ActivityAttendee, ActivityAttendeeResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<ApplicationUser, UserProfileResponse>()
            .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest => dest.FollowingsCount, opt => opt.MapFrom(src => src.Following.Count))
            .ForMember(dest => dest.Following, opt => opt.MapFrom(src => src.Followers.Any(x => x.FollowerId == currentUserId)))
            .ForMember(dest => dest.FollowedBy, opt => opt.MapFrom(src => src.Following.Any(x => x.FolloweeId == currentUserId)));

        CreateMap<ActivityComment, ActivityCommentResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<Photo, PhotoResponse>();
    }
}
