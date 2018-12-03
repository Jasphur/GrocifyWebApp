using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GrocifyAppMVC.Startup))]
namespace GrocifyAppMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
