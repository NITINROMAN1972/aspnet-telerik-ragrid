using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Telerik_Web_Forms_Site.Startup))]
namespace Telerik_Web_Forms_Site
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
