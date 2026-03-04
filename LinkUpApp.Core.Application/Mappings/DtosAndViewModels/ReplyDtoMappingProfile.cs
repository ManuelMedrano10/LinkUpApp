using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Reply;
using LinkUpApp.Core.Application.ViewModels.Reply;

namespace LinkUpApp.Core.Application.Mappings.DtosAndViewModels
{
    public class ReplyDtoMappingProfile : Profile
    {
        public ReplyDtoMappingProfile()
        {
            CreateMap<ReplyDto, ReplyViewModel>().ReverseMap();

            CreateMap<SaveReplyDto, SaveReplyViewModel>().ReverseMap();

            CreateMap<ReplyDto, DeleteReplyViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.CommetId, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Content, opt => opt.Ignore());
        }
    }
}
