using System.Diagnostics;
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
            configuration.Routes.MapHttpRoute("hello", "hello", new {controller = "Message", action = "Hello"});

            var container = BuildContainer();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            configuration.Filters.Add(new LogFilter());
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInfo>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<Stopwatch>().InstancePerLifetimeScope();
            return builder.Build();
        }
    }
}