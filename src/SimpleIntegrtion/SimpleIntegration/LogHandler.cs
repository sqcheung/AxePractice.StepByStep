using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace SimpleIntegration
{
    public class LogHandler: DelegatingHandler
    {
        readonly IMyLogger logger;

        public LogHandler(IMyLogger logger)
        {
            this.logger = logger;
        }

        protected override  async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IDependencyScope lifetimeScope = request.GetDependencyScope();
            var sw = (Stopwatch)lifetimeScope.GetService(typeof(Stopwatch));

            sw.Start();

            logger.Log($"{request.RequestUri} Request beginning....");
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            logger.Log($"{request.RequestUri} end, cycle time is {sw.Elapsed}");

            return response;
        }

    }
}