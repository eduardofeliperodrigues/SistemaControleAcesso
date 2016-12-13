using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaControleAcesso.Startup))]
namespace SistemaControleAcesso
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
