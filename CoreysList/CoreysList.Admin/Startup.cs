using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoreysList.Admin.Startup))]
namespace CoreysList.Admin
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
