using System;
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
        public static IContainer Init(HttpConfiguration configuration, Action<ContainerBuilder> customSetup)
        {
            configuration.Routes.MapHttpRoute("message", "message", new {controller = "Message", action = "Get"});
            configuration.Routes.MapHttpRoute("hello", "hello", new {controller = "Message", action = "Hello"});

            IContainer container = BuildContainer(customSetup);

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            configuration.Filters.Add(new LogFilter(container.Resolve<IMyLogger>()));

            return container;
        }

        static IContainer BuildContainer(Action<ContainerBuilder> customSetup)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInfo>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(l => new MyLogger(LogManager.GetLogger("mylogger"))).As<IMyLogger>().SingleInstance();
            builder.RegisterType<Stopwatch>().InstancePerLifetimeScope();

            customSetup(builder);
            IContainer container = builder.Build();
            return container;
        }
    }
}