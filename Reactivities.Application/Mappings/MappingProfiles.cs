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
        CreateMap<CreateActivityRequest, Activity>();
        CreateMap<UpdateActivityRequest, Activity>();

        CreateMap<Activity, ActivityResponse>();
        CreateMap<ActivityAttendee, ActivityAttendeeResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<ApplicationUser, UserProfileResponse>();

        CreateMap<ActivityComment, ActivityCommentResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<Photo, PhotoResponse>();
    }
}
