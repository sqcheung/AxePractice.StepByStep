using System;
using LocalApi;

namespace Manualfac.LocalApiIntegration
{
    public class ManualfacDependencyResolver : IDependencyResolver
    {
        #region Please implement the following class

        /*
         * We should create a manualfac dependency resolver so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */
        readonly ILifetimeScope rootScope;
        readonly IDependencyScope rootDependencyScope;

        public ManualfacDependencyResolver(Container rootScope)
        {
            this.rootScope = rootScope;
            rootDependencyScope = new ManualfacDependencyScope(rootScope);
        }

        public void Dispose()
        {
            rootScope.Dispose();
            rootDependencyScope.Dispose();
        }

        public object GetService(Type type)
        {
            return rootDependencyScope.GetService(type);
        }

        public IDependencyScope BeginScope()
        {
            return new ManualfacDependencyScope(rootScope.BeginLifetimeScope());
        }

        #endregion
    }
}