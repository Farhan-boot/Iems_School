using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Security.Claims;


namespace UI
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = ConfigurationManager.AppSettings["ida:Audience"]
                    },
                });

            //https://forums.asp.net/t/2056119.aspx?Change+default+ASP+NET+Identity+Two+factor+remember+Cookie+Expire+Time
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //    SlidingExpiration = true,
            //    ExpireTimeSpan = TimeSpan.FromHours(9),
            //    CookieDomain = domainName,
            //    CookieName = cookieName,
            //    Provider = new CookieAuthenticationProvider
            //    {
            //        // Enables the application to validate the security stamp when the user logs in.
            //        // This is a security feature which is used when you change a password or add an external login to your account.  
            //        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ProgenyUser, long>(
            //        validateInterval: TimeSpan.FromMinutes(30),
            //        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
            //        getUserIdCallback: (id) => (id.GetUserId<long>()))
            //    }
            //});



            //// Use a cookie to temporarily store information about a user logging in with a third party login provider
            ////app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //// Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            //// Enables the application to remember the second login verification factor such as phone or email.
            //// Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            //// This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //CookieAuthenticationOptions cookieAuthenticationOptions = new CookieAuthenticationOptions();
            //cookieAuthenticationOptions.AuthenticationType = DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie;
            //cookieAuthenticationOptions.AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive;
            //cookieAuthenticationOptions.CookieName = ".AspNet." + DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie;
            //cookieAuthenticationOptions.ExpireTimeSpan = TimeSpan.FromDays(30);
            //cookieAuthenticationOptions.SlidingExpiration = false;
            //CookieAuthenticationExtensions.UseCookieAuthentication(app, cookieAuthenticationOptions);




        }

       



    }
}
