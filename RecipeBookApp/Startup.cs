using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipeBookApp.Startup))]
namespace RecipeBookApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
