using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMB_Recording.Startup))]
namespace EMB_Recording
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
