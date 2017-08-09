using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace SimpleIntegration
{
    public class LogFilter: ActionFilterAttribute
    {
        readonly IMyLogger logger;

        public LogFilter(IMyLogger logger)
        {
            this.logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var sw = ResolveStopWatch(actionContext.Request);
            sw.Start();
            logger.Log($"Request beginning....{actionContext.Request.RequestUri}, swid: {sw.GetHashCode()}");
        }
        
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var sw = ResolveStopWatch(actionExecutedContext.Request);
            sw.Stop();
            
            logger.Log($"{actionExecutedContext.Request.RequestUri} cycle time is {sw.Elapsed}, swid: {sw.GetHashCode()}");
        }

        static Stopwatch ResolveStopWatch(HttpRequestMessage request)
        {
            IDependencyScope scope = request.GetDependencyScope();
            return (Stopwatch) scope.GetService(typeof(Stopwatch));
        }

    }

}