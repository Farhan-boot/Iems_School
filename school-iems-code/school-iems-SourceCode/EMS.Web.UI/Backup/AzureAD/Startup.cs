using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMS.Web.UI.Startup))]
namespace EMS.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
