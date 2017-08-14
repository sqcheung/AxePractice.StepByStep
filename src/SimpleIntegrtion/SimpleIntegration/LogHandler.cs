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

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IDependencyScope lifetimeScope = request.GetDependencyScope();
            var sw = (Stopwatch)lifetimeScope.GetService(typeof(Stopwatch));

            sw.Start();

            logger.Log($"{request.RequestUri} Request beginning....");

            return base.SendAsync(request, cancellationToken).ContinueWith(t =>
            {
                HttpResponseMessage result = t.Result;
                sw.Stop();
                logger.Log($"{request.RequestUri} end, cycle time is {sw.Elapsed}");
                return result;
            }, cancellationToken);
        }

    }
}