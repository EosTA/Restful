using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChatSystem.Api.Startup))]

namespace ChatSystem.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
