using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(LoginFacebookAndGoogle.Startup))]
namespace LoginFacebookAndGoogle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
