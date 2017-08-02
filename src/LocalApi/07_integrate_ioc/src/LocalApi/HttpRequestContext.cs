using System;
using System.Collections.Generic;
using LocalApi.Routing;

namespace LocalApi
{
    class HttpRequestContext : IDisposable
    {
        public HttpConfiguration Configuration { get; }
        public HttpRoute MatchedRoute { get; }
        
        public HttpRequestContext(HttpConfiguration configuration, HttpRoute matchedRoute)
        {
            Configuration = configuration;
            MatchedRoute = matchedRoute;
        }

        #region Please implement the following method

        /*
         * For each http context, at most one dependency scope will be created. In
         * this method, you should create and cache dependency scope.
         * 
         * Since the dependency scope manages all the object lifetimes. So we have
         * to dispose it when request context finished.
         * 
         * You can create non-public fields if needed.
         */
        IDependencyScope dependencyScope;
        public IDependencyScope GetDependencyScope()
        {
            dependencyScope = Configuration.DependencyResolver.BeginScope();
            return dependencyScope;
        }

        public void Dispose()
        {
            dependencyScope.Dispose();
        }

        #endregion
    }
}