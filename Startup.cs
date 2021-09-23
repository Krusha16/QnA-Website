using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QnA_Website.Startup))]
namespace QnA_Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
