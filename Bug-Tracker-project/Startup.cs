using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bug_Tracker_project.Startup))]
namespace Bug_Tracker_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
