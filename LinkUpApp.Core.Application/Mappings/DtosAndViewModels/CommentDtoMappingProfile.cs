using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Comment;
using LinkUpApp.Core.Application.ViewModels.Comment;

namespace LinkUpApp.Core.Application.Mappings.DtosAndViewModels
{
    public class CommentDtoMappingProfile : Profile
    {
        public CommentDtoMappingProfile()
        {
            CreateMap<CommentDto, CommentViewModel>()
                .ReverseMap();

            CreateMap<SaveCommentDto, SaveCommentViewModel>()
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CommentDto, DeleteCommentViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Content, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Replies, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.UserImageUrl, opt => opt.Ignore());
        }
    }
}
