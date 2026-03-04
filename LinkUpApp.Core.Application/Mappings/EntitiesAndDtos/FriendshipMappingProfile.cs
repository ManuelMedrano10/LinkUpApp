using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Friendship;
using LinkUpApp.Core.Domain.Entities;

namespace LinkUpApp.Core.Application.Mappings.EntitiesAndDtos
{
    public class FriendshipDtoMappingProfile : Profile
    {
        public FriendshipDtoMappingProfile()
        {
            CreateMap<Friendship, FriendshipDto>()
                .ForMember(dest => dest.OtherUserId, opt => opt.Ignore())
                .ForMember(dest => dest.OtherUserName, opt => opt.Ignore())
                .ForMember(dest => dest.OtherUserPhotoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Friendship, SaveFriendshipDto>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
