using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace SimpleIntegration
{
    public static class Bootstrapper
    {
        public static void Init(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("message", "message", new {controller = "Message", action = "Get"});

            var container = BuildContainer();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInfo>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            return builder.Build();
        }
    }
}