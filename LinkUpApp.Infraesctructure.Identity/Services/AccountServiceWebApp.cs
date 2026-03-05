using System.Text;
using LinkUpApp.Core.Application.Dtos.Email;
using LinkUpApp.Core.Application.Dtos.User;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Infraesctructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.Infraesctructure.Identity.Services
{
    public class AccountServiceWebApp : IAccountServiceWebApp
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountServiceWebApp
            (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginDto loginDto)
        {
            LoginResponseDto response = new()
            {
                Email = "",
                Id = "",
                UserName = "",
                Name = "",
                LastName = "",
                HasError = false,
                Errors = []
            };

            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Errors.Add($"There is not an account registered with the username provided: {loginDto.UserName}.");
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Errors.Add($"The account with the username {loginDto.UserName} is not active. " +
                                    $"Please check your email to your email to follow-up with the activation process.");
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName ?? "", loginDto.Password, false, true);

            if (!result.Succeeded)
            {
                response.HasError = true;
                if (result.IsLockedOut)
                {
                    response.Errors.Add($"Your account ({loginDto.UserName}) has been locked due to multiple failed attemps. " +
                                        $"Please try again in 20 minutes. If you do not remember your password, you can go " +
                                        $"through the reset process.");
                }
                else
                {
                    response.Errors.Add($"Your credentials are invalid for this account ({loginDto.UserName})");
                }
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email ?? "";
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.UserName = user.UserName ?? "";
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponseDto> RegisterUser(SaveUserDto saveUserDto, string origin)
        {
            RegisterResponseDto response = new()
            {
                Id = "",
                Email = "",
                Name = "",
                LastName = "",
                UserName = "",
                HasError = false,
                Errors = []
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(saveUserDto.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Errors.Add($"The username inserted ({saveUserDto.UserName}) is already used. Please choose another one.");
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(saveUserDto.Email);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Errors.Add($"The email inserted ({saveUserDto.Email}) is already used. Please choose another one.");
                return response;
            }

            AppUser user = new()
            {
                Name = saveUserDto.Name,
                LastName = saveUserDto.LastName,
                UserName = saveUserDto.UserName,
                Email = saveUserDto.Email,
                ProfileImage = saveUserDto.ProfileImage,
                EmailConfirmed = false,
                PhoneNumber = saveUserDto.Phone
            };

            var result = await _userManager.CreateAsync(user, saveUserDto.Password);
            if (result.Succeeded)
            {
                string verificationUri = await GetVerificationUri(user, origin);
                await _emailService.SendAsync(new EmailRequestDto
                {
                    To = saveUserDto.Email,
                    HtmlBody = $"Hello, thank you for your registration\n" +
                               $"\nPlease confirm your account before using our website by visiting" +
                               $"this URL: {verificationUri}.\n" +
                               $"\nSee you in LinkUp!!",
                    Subject = "Confirm Registration"
                });

                response.Id = user.Id;
                response.Email = user.Email ?? "";
                response.Name = user.Name;
                response.LastName = user.LastName;
                response.UserName = user.UserName ?? "";
                response.IsVerified = user.EmailConfirmed;

                return response;
            }
            else
            {
                response.HasError = true;
                response.Errors.AddRange(result.Errors.Select(s => s.Description).ToList());
                return response;
            }
        }

        public async Task<EditResponseDto> EditUser(SaveUserDto saveUserDto, string origin, bool? isCreated = false)
        {
            bool isNotCreated = !isCreated ?? false;
            EditResponseDto response = new()
            {
                Id = "",
                Email = "",
                Name = "",
                LastName = "",
                UserName = "",
                HasError = false,
                Errors = []
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(saveUserDto.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Errors.Add($"The username inserted ({saveUserDto.UserName}) is already used. Please choose another one.");
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(saveUserDto.Email);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Errors.Add($"The email inserted ({saveUserDto.Email}) is already used. Please choose another one.");
                return response;
            }

            var user = await _userManager.FindByIdAsync(saveUserDto.Id);

            if (user == null)
            {
                response.HasError = true;
                response.Errors.Add($"There is NOT an account registered with this user.");
                return response;
            }

            user.Name = saveUserDto.Name;
            user.LastName = saveUserDto.LastName;
            user.UserName = saveUserDto.UserName;
            user.Email = saveUserDto.Email;
            user.ProfileImage = saveUserDto.ProfileImage;
            user.EmailConfirmed = user.EmailConfirmed && user.Email == saveUserDto.Email;
            user.PhoneNumber = saveUserDto.Phone;

            if (!string.IsNullOrWhiteSpace(saveUserDto.Password) && isNotCreated)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resultChange = await _userManager.ResetPasswordAsync(user, token, saveUserDto.Password);

                if (resultChange != null && !resultChange.Succeeded)
                {
                    response.HasError = true;
                    response.Errors.AddRange(resultChange.Errors.Select(s => s.Description).ToList());
                    return response;
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!user.EmailConfirmed && isNotCreated)
                {
                    string verificationUri = await GetResetPasswordUri(user, origin);
                    await _emailService.SendAsync(new EmailRequestDto
                    {
                        To = saveUserDto.Email,
                        HtmlBody = $"Hello, happy to see you again!\n" +
                               $"\nPlease confirm your account before using our website by visiting" +
                               $"this URL: {verificationUri}.\n" +
                               $"\nSee you in LinkUp!!",
                        Subject = "Confirm Registration"
                    });
                }

                response.Id = user.Id;
                response.Email = user.Email ?? "";
                response.UserName = user.UserName ?? "";
                response.Name = user.Name;
                response.LastName = user.LastName;
                response.IsVerified = user.EmailConfirmed;

                return response;
            }
            else
            {
                response.HasError = true;
                response.Errors.AddRange(result.Errors.Select(s => s.Description).ToList());
                return response;
            }
        }

        public async Task<UserResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {
            UserResponseDto response = new()
            {
                HasError = false,
                Errors = []
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Errors.Add($"There is not an account registered with the username provided: {request.UserName}.");
                return response;
            }

            var resetUri = await GetResetPasswordUri(user, request.Origin);
            user.EmailConfirmed = false;

            await _userManager.UpdateAsync(user);

            await _emailService.SendAsync(new EmailRequestDto
            {
                To = user.Email,
                HtmlBody = $"Hello, happy to see you again!\n" +
                               $"\nPlease reset your password by visiting this URL: {resetUri}.\n" +
                               $"\nSee you in LinkUp!!",
                Subject = "Reset Password"
            });

            return response;
        }

        public async Task<UserResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            UserResponseDto response = new()
            {
                HasError = false,
                Errors = []
            };

            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                response.HasError = true;
                response.Errors.Add($"There is NOT an account registered with this user.");
                return response;
            }

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Errors.AddRange(result.Errors.Select(s => s.Description).ToList());
                return response;
            }

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            return response;
        }

        public async Task<UserResponseDto> DeleteAsync(string id)
        {
            UserResponseDto response = new()
            {
                HasError = false,
                Errors = []
            };

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                response.HasError = true;
                response.Errors.Add($"There is NOT an account registered with this user.");
                return response;
            }

            await _userManager.DeleteAsync(user);

            return response;
        }

        public async Task<UserDto?> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }


            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email ?? "",
                LastName = user.LastName,
                Name = user.Name,
                UserName = user.UserName ?? "",
                ProfileImage = user.ProfileImage,
                Phone = user.PhoneNumber,
                IsVerified = user.EmailConfirmed
            };

            return userDto;
        }

        public async Task<UserDto?> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email ?? "",
                LastName = user.LastName,
                Name = user.Name,
                UserName = user.UserName ?? "",
                ProfileImage = user.ProfileImage,
                Phone = user.PhoneNumber,
                IsVerified = user.EmailConfirmed
            };

            return userDto;
        }

        public async Task<UserDto?> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email ?? "",
                LastName = user.LastName,
                Name = user.Name,
                UserName = user.UserName ?? "",
                ProfileImage = user.ProfileImage,
                Phone = user.PhoneNumber,
                IsVerified = user.EmailConfirmed
            };

            return userDto;
        }

        public async Task<List<UserDto>?> GetAllUsers(bool? isActive = true)
        {
            List<UserDto> listUserDtos = [];

            var users = _userManager.Users;

            if (isActive != null && isActive == true)
            {
                users = users.Where(w => w.EmailConfirmed);
            }

            var listUser = await users.ToListAsync();

            foreach (var item in listUser)
            {

                listUserDtos.Add(new UserDto()
                {
                    Id = item.Id,
                    Email = item.Email ?? "",
                    LastName = item.LastName,
                    Name = item.Name,
                    UserName = item.UserName ?? "",
                    ProfileImage = item.ProfileImage,
                    Phone = item.PhoneNumber,
                    IsVerified = item.EmailConfirmed
                });
            }

            return listUserDtos;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "There is no acccount registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"The account is confirmed for {user.Email}. You can now use the app freely.";
            }
            else
            {
                return $"An error occurred while confirming this email {user.Email}";
            }
        }

        #region Private Methods
        private async Task<string> GetVerificationUri(AppUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "Login/ConfirmEmail";
            var completeUrl = new Uri(string.Concat(origin, "/", route));
            var verificationUri = QueryHelpers.AddQueryString(completeUrl.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri.ToString(), "token", token);

            return verificationUri;
        }

        private async Task<string> GetResetPasswordUri(AppUser user, string origin)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "Login/ResetPassword";
            var completeUrl = new Uri(string.Concat(origin, "/", route));
            var resetUri = QueryHelpers.AddQueryString(completeUrl.ToString(), "userId", user.Id);
            resetUri = QueryHelpers.AddQueryString(resetUri.ToString(), "token", token);

            return resetUri;
        }
        #endregion
    }
}
