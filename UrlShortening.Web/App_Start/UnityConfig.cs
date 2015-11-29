using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;
using UrlShortening.Infrastructure.IInfrastructure;
using UrlShortening.Infrastructure.Infrastraucture;
using UrlShortening.Repository.IRepository;
using UrlShortening.Repository.Repository;
using UrlShortening.Service.IService;
using UrlShortening.Service.Service;

namespace UrlShortening.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            /*******************   REPOSITORY Registration *****************************/
            container.RegisterType<IConnection, Connection>()
                .RegisterType<IUrlRepository, UrlRepository>();

            /*******************   SERVICE Registration *****************************/
            container.RegisterType<IConversionService, ConversionService>()
                .RegisterType<IUrlService, UrlService>();
            
             /*******************   INFRASTRUCTURE Registration *****************************/
            container.RegisterType<ILogger, Logger>()
                .RegisterType<IConfiguration, Configuration>();
            
            
            // Dependency for MVC
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            
            // Dependency for WebApi
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}