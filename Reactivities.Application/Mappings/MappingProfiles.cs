using AutoMapper;
using Reactivities.Application.Models.Request.Activities;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Domain;

namespace Reactivities.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateActivityRequest, Activity>();
        CreateMap<UpdateActivityRequest, Activity>();
        CreateMap<Activity, ActivityResponse>();
    }
}
