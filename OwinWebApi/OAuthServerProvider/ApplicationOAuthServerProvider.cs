using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using OwinWebApi.Models;
using OwinWebApi.Services;

namespace OwinWebApi.OAuthServerProvider
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {


        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserStore store = new UserStore(new UserDbContext());
            User user = await store.FindByEmailAsync(context.UserName);

            if (user == null || !store.IsPasswordValid(user,context.Password))
            {
                context.SetError("invalid_grant",
                    "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            foreach (var claim in user.Claims)
            {
                identity.AddClaim(new Claim(claim.ClaimType,claim.ClaimValue));
            }

            context.Validated(identity);
        }
    }
}
