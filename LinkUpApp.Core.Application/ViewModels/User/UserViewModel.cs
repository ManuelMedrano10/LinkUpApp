namespace LinkUpApp.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Phone { get; set; }
        public required string ProfileImage { get; set; }
        public bool? IsVerified { get; set; }
        public required string Role { get; set; }
    }
}
