using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Post;
using LinkUpApp.Core.Domain.Entities;

namespace LinkUpApp.Core.Application.Mappings.EntitiesAndDtos
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<Post, SavePostDto>()
                .ReverseMap()
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Reactions, opt => opt.Ignore());
        }
    }
}
