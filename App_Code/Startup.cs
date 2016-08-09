using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebFormExample.Startup))]
namespace WebFormExample
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
