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
        CreateMap<Hobby, HobbyDto>();
        CreateMap<Tag, TagDto>();
        CreateMap<InfoDetails, InfoDto>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.InfoId))
            .ForMember(dest => dest.Title, opt =>
                opt.MapFrom(src => src.Info.Title))
            .ForMember(dest => dest.Answers, opt =>
                opt.MapFrom(src => new List<AnswerDto>
                {
                    new AnswerDto
                    {
                        Id = src.InfoAnswer.Id,
                        Answer = src.InfoAnswer.Answer
                    }
                }));

        CreateMap<UserDetails, ProfileDto>();
        CreateMap<User, ProfileDto>()
            .IncludeMembers(src => src.UserDetails)
            // .ForMember(dest => dest.Hobby, opt =>
            //     opt.MapFrom(src => src.UserDetails!.Hobbies.Select(h => h.HobbyName).ToList()))
            .ForMember(dest => dest.Location, opt => 
                opt.MapFrom(src => new LocationDto{Longitude = src.UserDetails!.LastLocation!.X, Latitude = src.UserDetails.LastLocation.Y}))
            .ForMember(dest => dest.Gender, opt => 
                opt.MapFrom(src => src.UserDetails!.Gender.ToString().ToUpperInvariant()))
            .ForMember(dest => dest.Info, opt => 
                opt.MapFrom(src => src.UserDetails!.InfoDetails));

        CreateMap<Preference, PreferenceDto>()
            .ForMember(dest => dest.Gender, opt => 
                opt.MapFrom(src => src.Gender.ToString().ToUpperInvariant()));
        CreateMap<UpdatePreferenceDto, Preference>()
            .ForMember(dest => dest.Gender, opt => 
                opt.MapFrom(src => (GenderPreference)Enum.Parse(typeof(GenderPreference), src.Gender.ToPascalCase())));
    }
}