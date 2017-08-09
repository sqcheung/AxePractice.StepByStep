using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using NLog;

namespace SimpleIntegration
{
    public static class Bootstrapper
    {
        public static void Init(HttpConfiguration configuration, IContainer container)
        {
            configuration.Routes.MapHttpRoute("message", "message", new {controller = "Message", action = "Get"});
            configuration.Routes.MapHttpRoute("hello", "hello", new {controller = "Message", action = "Hello"});

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            configuration.Filters.Add(new LogFilter(container.Resolve<IMyLogger>()));
        }

        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInfo>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(l => new MyLogger(LogManager.GetLogger("mylogger"))).As<IMyLogger>().SingleInstance();
            builder.RegisterType<Stopwatch>().InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}