using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UrlShortening.Repository.Mapping;
using UrlShortening.Web.Controllers;
using UrlShortening.Web.Models.Mapping;

namespace UrlShortening.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Register DI components
            UnityConfig.RegisterComponents();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();
            
            // Register Model mappings
            MapperWebConfiguration.Configure();
            MapperRepositoryConfiguration.Configure();

            // Register Logger
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
