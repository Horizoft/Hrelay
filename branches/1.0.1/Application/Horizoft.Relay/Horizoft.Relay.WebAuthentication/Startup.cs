using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Horizoft.Relay.WebAuthentication.Startup))]
namespace Horizoft.Relay.WebAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
