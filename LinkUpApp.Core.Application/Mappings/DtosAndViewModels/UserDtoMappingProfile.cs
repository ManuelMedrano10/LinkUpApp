using AutoMapper;
using LinkUpApp.Core.Application.Dtos.User;
using LinkUpApp.Core.Application.ViewModels.User;

namespace LinkUpApp.Core.Application.Mappings.DtosAndViewModels
{
    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<UserDto, UpdateUserViewModel>()
                .ForMember(dest => dest.ProfileImageFile, opt => opt.Ignore())
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Errors, opt => opt.Ignore());

            CreateMap<SaveUserDto, UpdateUserViewModel>()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImage));

            CreateMap<SaveUserDto, RegisterUserViewModel>()
                .ForMember(dest => dest.ProfileImageFile, opt => opt.MapFrom(src => src.ProfileImage))
                .ReverseMap();

            CreateMap<SaveUserDto, CreateUserViewModel>()
                .ForMember(dest => dest.ProfileImageFile, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());

            CreateMap<LoginDto, LoginViewModel>()
                .ReverseMap();
        }
    }
}
