using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;

namespace SimpleIntegration.Test
{
    public class TestBase: IDisposable
    {
        protected IContainer container;
        readonly HttpServer httpServer;
        public HttpClient Client { get; }


        public TestBase()
        {
            var configuration = new HttpConfiguration();

            container = Bootstrapper.Init(configuration,
                builder => builder.Register(l => new FakeMyLogger()).As<IMyLogger>().SingleInstance());

            httpServer = new HttpServer(configuration);

            Client = CreatHttpClient(httpServer);
        }

        static HttpClient CreatHttpClient(HttpServer httpServer)
        {
            var httpClient = new HttpClient(httpServer);
            return httpClient;
        }

        public void Dispose()
        {
            httpServer?.Dispose();
            Client?.Dispose();
        }
    }
}