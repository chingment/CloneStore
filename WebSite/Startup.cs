using Microsoft.Owin;
using Newtonsoft.Json.Converters;
using Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(WebSite.Startup))]
namespace WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
    

            ConfigureAuth(app);
        }
    }
}
