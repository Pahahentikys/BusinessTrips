using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusinessTrips.Startup))]
namespace BusinessTrips
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
