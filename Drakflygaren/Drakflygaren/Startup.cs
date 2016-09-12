using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Drakflygaren.Startup))]
namespace Drakflygaren
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
