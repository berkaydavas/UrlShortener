using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UrlShortener.Startup))]
namespace UrlShortener
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
