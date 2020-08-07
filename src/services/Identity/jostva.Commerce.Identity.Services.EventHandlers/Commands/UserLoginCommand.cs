using jostva.Commerce.Identity.Services.EventHandlers.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace jostva.Commerce.Identity.Services.EventHandlers.Commands
{
    public class UserLoginCommand : IRequest<IdentityAccess>
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}