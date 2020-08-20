using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginFacebookAndGoogle
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            #region Facebook
            var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings["FacebookAuthenticationAppId"],
                AppSecret = ConfigurationManager.AppSettings["FacebookAuthenticationAppSecret"],
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        await Task.Run(() =>
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                            #region codigo que puede ser Util
                            //foreach (var claim in context.User)
                            //{
                            //    var claimType = string.Format("urn:facebook:{0}", claim.Key);
                            //    string claimValue = claim.Value.ToString();
                            //    if (!context.Identity.HasClaim(claimType, claimValue))
                            //        context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));
                            //}
                            #endregion
                        });
                      }
                }
            };
            facebookAuthenticationOptions.Scope.Add("public_profile");
            facebookAuthenticationOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookAuthenticationOptions);
            #endregion Facebook

            #region Google

            var googleAuthenticationOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = ConfigurationManager.AppSettings["GoogleAppId"],
                ClientSecret = ConfigurationManager.AppSettings["GoogleAppSecret"],
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                     OnAuthenticated =  async context =>
                    {
                        await Task.Run(() => {
                        context.Identity.AddClaim(new Claim("GoogleAccessToken", context.AccessToken));
                        context.Identity.AddClaim(new Claim("GoogleInfo", context.User.ToString()));
                            #region codigo que puede ser Util
                            //foreach (var claim in context.User)
                            //{
                            //    var claimType = string.Format("urn:google:{0}", claim.Key);
                            //    string claimValue = claim.Value.ToString();
                            //    if (!context.Identity.HasClaim(claimType, claimValue))
                            //        context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Google"));
                            //}
                            #endregion
                        });
                    }
                }
            };
            //googleAuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.login email");
            app.UseGoogleAuthentication(googleAuthenticationOptions);

            #endregion Google
        }
    }
}