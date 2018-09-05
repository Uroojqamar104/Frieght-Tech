using FreightTech.Api.App_Start;
using FreightTech.Api.Filters;
using FreightTech.Data.Repositories;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Unity;
using Unity.Lifetime;

namespace FreightTech.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDriverRepository, DriverRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAppLogger, AppLogger>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);


            // There can be multiple exception loggers.
            // (By default, no exception loggers are registered.)
            config.Services.Add(typeof(IExceptionLogger), new AppExceptionLogger());

            // There must be exactly one exception handler.
            // (There is a default one that may be replaced.)
           config.Services.Replace(typeof(IExceptionHandler), new GenericTextExceptionHandler());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
