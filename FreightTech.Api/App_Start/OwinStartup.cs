using FreightTech.Data.Repositories;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(FreightTech.Api.App_Start.OwinStartup))]
namespace FreightTech.Api.App_Start
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = GetUserRepository().GetUserProfile(context.UserName);
            if (user == null)
            {
                AppLogger.Instance.Warning(0, "invalid_grant: The user name or password is incorrect.");
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            //if (user.IsBlocked)
            //{
            //    context.SetError("invalid", "Sorry you are blocked :D .");
            //    return;
            //}
            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

            //    if (user == null)
            //    {
            //        context.SetError("invalid_grant", "The user name or password is incorrect.");
            //        return;
            //    }
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("UserName", context.UserName));
            identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
            identity.AddClaim(new Claim("Role", ((Enum.UserRole)user.RoleId).ToString()));

            context.Validated(identity);
            AppLogger.Instance.Info(user.UserId, "Token created successfully.");
        }

        UserRepository GetUserRepository()
        {
            return UserRepository.Instance;
        }
    }

}