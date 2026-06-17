using AutoMapper;
using Reactivities.Application.Models.Request.Activities;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Attendees;
using Reactivities.Application.Models.Response.Identity;
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

        CreateMap<ActivityAttendee, AttendeesResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<ApplicationUser, UserResponse>();
    }
}
