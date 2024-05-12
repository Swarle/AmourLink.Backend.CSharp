using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.DTO;
using AutoMapper;

namespace AmourLink.Recommendation.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Picture, PictureDto>();
        CreateMap<Degree, DegreeDto>();
        CreateMap<UserDetails, MemberDto>();
        CreateMap<User, MemberDto>()
            .IncludeMembers(src => src.UserDetails)
            .ForMember(dest => dest.Hobbies, opt =>
                opt.MapFrom(src => src.UserDetails!.Hobbies.Select(h => h.HobbieName).ToList()));
    }
}