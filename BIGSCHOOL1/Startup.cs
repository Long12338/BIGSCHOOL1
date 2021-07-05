using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BIGSCHOOL1.Startup))]
namespace BIGSCHOOL1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
