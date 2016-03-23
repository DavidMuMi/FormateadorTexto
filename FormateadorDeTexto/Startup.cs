using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FormateadorDeTexto.Startup))]
namespace FormateadorDeTexto
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
