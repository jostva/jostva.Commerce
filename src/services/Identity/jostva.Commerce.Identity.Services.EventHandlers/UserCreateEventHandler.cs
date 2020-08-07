using jostva.Commerce.Identity.Domain;
using jostva.Commerce.Identity.Services.EventHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace jostva.Commerce.Identity.Services.EventHandlers
{
    public class UserCreateEventHandler : IRequestHandler<UserCreateCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> userManager;


        public UserCreateEventHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<IdentityResult> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser entry = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            return await userManager.CreateAsync(entry, request.Password);
        }
    }
}