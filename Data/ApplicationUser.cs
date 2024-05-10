using Microsoft.AspNetCore.Identity;

namespace JobNest.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEmployer { get; set; }
    }
}
