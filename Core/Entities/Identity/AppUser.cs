using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    // IdentityUser comes from AspNetCore.Identity;
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }   
    }
}