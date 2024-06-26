using AmourLink.Infrastructure.Extensions;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Data.Entities.Enums;
using AmourLink.Recommendation.DTO;
using AutoMapper;

namespace AmourLink.Recommendation.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Picture, PictureDto>();
        CreateMap<Degree, DegreeDto>();
        CreateMap<UserDetails, MemberDto>();
        CreateMap<User, MemberDto>()
            .IncludeMembers(src => src.UserDetails)
            .ForMember(dest => dest.Hobby, opt =>
                opt.MapFrom(src => src.UserDetails!.Hobbies.Select(h => h.HobbyName).ToList()))
            .ForMember(dest => dest.Location, opt => 
                opt.MapFrom(src => new LocationDto{Longitude = src.UserDetails!.LastLocation!.X, Latitude = src.UserDetails.LastLocation.Y}))
            .ForMember(src => src.Tags, opt =>
                opt.MapFrom(dest => dest.UserDetails!.Tags.Select(t => t.TagName).ToList()))
            .ForMember(dest => dest.Gender, opt => 
                opt.MapFrom(src => src.UserDetails!.Gender.ToString().ToUpperInvariant()));

        CreateMap<Preference, PreferenceDto>()
            .ForMember(dest => dest.Gender, opt => 
                opt.MapFrom(src => src.Gender.ToString().ToUpperInvariant()));
        CreateMap<UpdatePreferenceDto, Preference>()
            .ForMember(dest => dest.Gender, opt => 
                opt.MapFrom(src => (GenderPreference)Enum.Parse(typeof(GenderPreference), src.Gender.ToPascalCase())));
    }
}