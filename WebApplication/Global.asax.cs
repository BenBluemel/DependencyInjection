using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication.Controllers;
using WebApplication.IoC;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependencyInjector.Instance.Container.Register<HomeController, HomeController>();
            ControllerBuilder.Current.SetControllerFactory(typeof(IoCControllerFactory));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
