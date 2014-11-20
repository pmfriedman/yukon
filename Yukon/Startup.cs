using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Yukon.Startup))]
namespace Yukon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
