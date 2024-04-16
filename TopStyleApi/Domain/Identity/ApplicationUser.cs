using Microsoft.AspNetCore.Identity;
using TopStyle.Domain.Entities;

namespace TopStyle.Domain.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
