using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Post;
using LinkUpApp.Core.Application.ViewModels.Post;

namespace LinkUpApp.Core.Application.Mappings.DtosAndViewModels
{
    public class PostDtoMappingProfile : Profile
    {
        public PostDtoMappingProfile()
        {
            CreateMap<PostDto, PostViewModel>().ReverseMap();
            CreateMap<SavePostDto, SavePostViewModel>().ReverseMap();
            CreateMap<PostDto, DeletePostViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Content, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.VideoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.UserPhotoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());
        }
    }
}
