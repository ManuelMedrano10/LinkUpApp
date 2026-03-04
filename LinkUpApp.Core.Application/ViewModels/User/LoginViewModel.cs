using System.ComponentModel.DataAnnotations;

namespace LinkUpApp.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must enter the username.")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You must enter the password.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
