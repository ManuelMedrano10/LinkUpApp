using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Friendship;
using LinkUpApp.Core.Application.ViewModels.Friendship;

namespace LinkUpApp.Core.Application.Mappings.DtosAndViewModels
{
    public class FriendshipDtoMappingProfile : Profile
    {
        public FriendshipDtoMappingProfile()
        {
            CreateMap<FriendshipDto, FriendshipViewModel>().ReverseMap();
        }
    }
}
