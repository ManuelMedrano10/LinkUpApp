using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LinkUpApp.Core.Application.ViewModels.User
{
    public class CreateUserViewModel
    {
        public required int Id { get; set; }

        [Required(ErrorMessage = "You must enter the name of the user.")]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "You must enter the last name of the user.")]
        [DataType(DataType.Text)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "You must enter the email of the user.")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "You must enter the username of the user.")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You must enter the password of the user.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password must match.")]
        [Required(ErrorMessage = "You must enter the confirm password.")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        public required string Phone { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "You must enter the profile image of the user.")]
        public IFormFile? ProfileImageFile { get; set; }
    }
}
