using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GalaxyComputersASP.Startup))]
namespace GalaxyComputersASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
