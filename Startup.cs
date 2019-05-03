using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookshop_CATeam11.Startup))]
namespace Bookshop_CATeam11
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
