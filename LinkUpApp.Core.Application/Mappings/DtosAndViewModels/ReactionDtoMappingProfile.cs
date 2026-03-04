using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Reaction;
using LinkUpApp.Core.Application.ViewModels.Reaction;

namespace LinkUpApp.Core.Application.Mappings.DtosAndViewModels
{
    public class ReactionDtoMappingProfile : Profile
    {
        public ReactionDtoMappingProfile()
        {
            CreateMap<ReactionDto, ReactionViewModel>().ReverseMap();

            CreateMap<SaveReactionDto, SaveReactionViewModel>().ReverseMap();

            CreateMap<ReactionDto, DeleteReactionViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.IsLiked, opt => opt.Ignore())
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore());
        }
    }
}
