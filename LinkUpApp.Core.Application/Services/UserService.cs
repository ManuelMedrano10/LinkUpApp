using AutoMapper;
using LinkUpApp.Core.Application.Dtos.User;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Application.ViewModels.User;

namespace LinkUpApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountServiceWebApp _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountServiceWebApp accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<UpdateUserViewModel?> GetUserForEditAsync(string userId)
        {
            var userDto = await _accountService.GetUserById(userId);

            if (userDto == null) 
            {
                return null;
            }

            var viewModel = _mapper.Map<UpdateUserViewModel>(userDto);

            viewModel.Password = string.Empty;
            viewModel.ConfirmPassword = string.Empty;

            return viewModel;
        }

        public async Task<EditResponseDto> UpdateProfileAsync(SaveUserDto saveDto, string origin)
        {
            var currentUser = await _accountService.GetUserById(saveDto.Id);

            if (currentUser == null)
            {
                return new EditResponseDto 
                {
                    Id = "",
                    Name = "",
                    LastName = "",
                    Email = "",
                    UserName = "",
                    HasError = true, 
                    Errors = ["The user was not founded."] 
                };
            }

            saveDto.UserName = currentUser.UserName;
            saveDto.Email = currentUser.Email;

            if (string.IsNullOrEmpty(saveDto.ProfileImage) && !string.IsNullOrEmpty(currentUser.ProfileImage))
            {
                saveDto.ProfileImage = currentUser.ProfileImage;
            }

            return await _accountService.EditUser(saveDto, origin, false);
        }
    }
}
