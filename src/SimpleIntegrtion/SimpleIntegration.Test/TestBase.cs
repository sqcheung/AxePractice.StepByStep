using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http;
using Autofac;

namespace SimpleIntegration.Test
{
    public class TestBase: IDisposable
    {
        readonly HttpServer httpServer;
        protected IContainer buildContainer;

        public TestBase()
        {
            buildContainer = BuildContainer();
            httpServer = CreatHttpServer(buildContainer);
            Client = CreatHttpClient(httpServer);
        }

        public HttpClient Client { get; }

        static HttpClient CreatHttpClient(HttpServer httpServer)
        {
            var httpClient = new HttpClient(httpServer);
            return httpClient;
        }

        static HttpServer CreatHttpServer(IContainer buildContainer)
        {
            var configuration = new HttpConfiguration();

            Bootstrapper.Init(configuration, buildContainer);
            var httpServer = new HttpServer(configuration);
            return httpServer;
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInfo>();
            builder.RegisterType<MessageController>();
            builder.RegisterType<Stopwatch>().InstancePerLifetimeScope();
            builder.Register(l => new FakeMyLogger()).As<IMyLogger>().SingleInstance();
            IContainer container = builder.Build();
            return container;
        }

        public void Dispose()
        {
            httpServer?.Dispose();
            Client?.Dispose();
        }
    }
}