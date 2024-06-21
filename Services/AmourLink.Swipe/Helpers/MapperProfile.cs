using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Swipe.Data.Entities;
using AmourLink.Swipe.DTO;
using AutoMapper;

namespace AmourLink.Swipe.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<SwipeEntity, UserSentLikeDto>()
            .ForMember(dest => dest.SwipeType, opt =>
                opt.MapFrom(src => src.SwipeType.ToString()));
    }
}