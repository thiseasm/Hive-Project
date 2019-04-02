using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HiveProject.Startup))]
namespace HiveProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
