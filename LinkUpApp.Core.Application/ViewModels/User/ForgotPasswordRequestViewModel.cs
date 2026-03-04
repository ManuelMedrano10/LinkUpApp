using System.ComponentModel.DataAnnotations;

namespace LinkUpApp.Core.Application.ViewModels.User
{
    public class ForgotPasswordRequestViewModel
    {
        [Required(ErrorMessage = "You must enter the username.")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }
    }
}
