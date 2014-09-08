using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvcDemo.Startup))]
namespace mvcDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
