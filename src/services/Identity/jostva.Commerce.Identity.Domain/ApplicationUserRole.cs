using Microsoft.AspNetCore.Identity;

namespace jostva.Commerce.Identity.Domain
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationRole Role { get; set; }
        
        public ApplicationUser User { get; set; }
    }
}