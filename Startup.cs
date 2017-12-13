using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PandaTours.Startup))]
namespace PandaTours
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
