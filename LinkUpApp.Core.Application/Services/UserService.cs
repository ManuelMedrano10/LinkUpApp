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

        public async Task<EditResponseDto> UpdateProfileAsync(UpdateUserViewModel vm, string origin)
        {
            var currentUser = await _accountService.GetUserById(vm.Id);

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
                    Errors = ["User not founded."] };
            }

            var saveUserDto = _mapper.Map<SaveUserDto>(vm);

            saveUserDto.UserName = currentUser.UserName;
            saveUserDto.Email = currentUser.Email;

            if (vm.ProfileImageFile == null && !string.IsNullOrEmpty(currentUser.ProfileImage))
            {
                saveUserDto.ProfileImage = currentUser.ProfileImage;
            }

            return await _accountService.EditUser(saveUserDto, origin, false);
        }
    }
}
