using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Comment;
using LinkUpApp.Core.Application.Dtos.Post;
using LinkUpApp.Core.Domain.Entities;

namespace LinkUpApp.Core.Application.Mappings.EntitiesAndDtos
{
    public class CommentDtoMappingProfile : Profile
    {
        public CommentDtoMappingProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.UserImageUrl, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore());


            CreateMap<Comment, SaveCommentDto>()
                .ReverseMap()
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Replies, opt => opt.Ignore());
        }
    }
}
