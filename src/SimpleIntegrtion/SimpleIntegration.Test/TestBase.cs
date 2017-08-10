using System;
using System.Net.Http;
using System.Web.Http;

namespace SimpleIntegration.Test
{
    public class TestBase: IDisposable
    {
        readonly HttpServer httpServer;
        public HttpClient Client { get; }


        public TestBase()
        {
            httpServer = CreatHttpServer();
            Client = CreatHttpClient(httpServer);
        }

        static HttpClient CreatHttpClient(HttpServer httpServer)
        {
            var httpClient = new HttpClient(httpServer);
            return httpClient;
        }

        static HttpServer CreatHttpServer()
        {
            var configuration = new HttpConfiguration();

            Bootstrapper.Init(configuration);
            var httpServer = new HttpServer(configuration);
            return httpServer;
        }

        public void Dispose()
        {
            httpServer?.Dispose();
            Client?.Dispose();
        }
    }
}