using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeetMeUp_Updated.Startup))]
namespace MeetMeUp_Updated
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
