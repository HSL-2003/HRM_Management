using Microsoft.AspNetCore.Identity;

namespace hrmana.HRManagement.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
