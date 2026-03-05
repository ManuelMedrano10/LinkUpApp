using LinkUpApp.Core.Application.Dtos.User;
using LinkUpApp.Core.Application.ViewModels.User;

namespace LinkUpApp.Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<UpdateUserViewModel?> GetUserForEditAsync(string userId);
        Task<EditResponseDto> UpdateProfileAsync(UpdateUserViewModel vm, string origin);
    }
}
