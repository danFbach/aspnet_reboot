using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP_Reboot.Startup))]
namespace ASP_Reboot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
