using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DistanceCalculatorWithCoords.Startup))]
namespace DistanceCalculatorWithCoords
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
