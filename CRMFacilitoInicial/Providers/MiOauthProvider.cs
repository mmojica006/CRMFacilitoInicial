using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using CRMFacilitoInicial.Models;
using System.Security.Claims;

namespace CRMFacilitoInicial.Providers
{
    public class MiOauthProvider: OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var UserManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser usuario = await UserManager.FindAsync(context.UserName, context.Password);
            if (usuario == null)
            {
                context.SetError("Acceso inválido", "El nombre de usuario o la contraseña son incorrectas");
                return;
            }

            ClaimsIdentity oAuthIdentity = await UserManager.CreateIdentityAsync(usuario, OAuthDefaults.AuthenticationType);
            context.Validated(oAuthIdentity);

          



        }

        public override  Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();

            return Task.FromResult<object>(null);
            
        }

    }
}