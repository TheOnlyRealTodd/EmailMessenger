using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactFormWithEmail.Startup))]
namespace ContactFormWithEmail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
