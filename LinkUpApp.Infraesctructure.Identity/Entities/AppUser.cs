using Microsoft.AspNetCore.Identity;

namespace LinkUpApp.Infraesctructure.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string ProfileImage { get; set; }
    }
}
