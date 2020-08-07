using jostva.Commerce.Identity.Data;
using jostva.Commerce.Identity.Domain;
using jostva.Commerce.Identity.Services.EventHandlers.Commands;
using jostva.Commerce.Identity.Services.EventHandlers.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace jostva.Commerce.Identity.Services.EventHandlers
{
    public class UserLoginEventHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;


        public UserLoginEventHandler(SignInManager<ApplicationUser> signInManager,
                                     ApplicationDbContext context,
                                     IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.context = context;
            this.configuration = configuration;
        }


        public async Task<IdentityAccess> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            IdentityAccess result = new IdentityAccess();

            ApplicationUser user = await context.Users.SingleAsync(x => x.Email == request.Email);
            SignInResult response = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (response.Succeeded)
            {
                result.Succeeded = true;
                await GenerateToken(user, result);
            }

            return result;
        }


        private async Task GenerateToken(ApplicationUser user, IdentityAccess identity)
        {
            string secretKey = configuration.GetValue<string>("SecretKey");
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            List<ApplicationRole> roles = await context.Roles
                                                       .Where(x => x.UserRoles.Any(y => y.UserId == user.Id))
                                                       .ToListAsync();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken createdToken = tokenHandler.CreateToken(tokenDescriptor);

            identity.AccessToken = tokenHandler.WriteToken(createdToken);
        }
    }
}