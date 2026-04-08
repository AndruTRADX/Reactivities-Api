using AutoMapper;
using Reactivities.Application.Models.Response;
using Reactivities.Domain;

namespace Reactivities.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Activity, Activity>();
        CreateMap<Activity, ActivityResponse>();
    }
}
