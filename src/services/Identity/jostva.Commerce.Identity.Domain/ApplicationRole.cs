using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace jostva.Commerce.Identity.Domain
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}