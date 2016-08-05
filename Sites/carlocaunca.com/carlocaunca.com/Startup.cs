using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(carlocaunca.com.Startup))]
namespace carlocaunca.com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
