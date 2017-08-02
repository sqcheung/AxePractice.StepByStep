using System;
using LocalApi;
using Manualfac.Services;

namespace Manualfac.LocalApiIntegration
{
    class ManualfacDependencyScope : IDependencyScope
    {

        #region Please implement the class

        /*
         * We should create a manualfac dependency scope so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */
        readonly ILifetimeScope lifeTimeScope;

        public ManualfacDependencyScope(ILifetimeScope lifeTimeScope)
        {
            this.lifeTimeScope = lifeTimeScope;
        }

        public void Dispose()
        {
            lifeTimeScope.Dispose();
        }

        public object GetService(Type type)
        {
            return lifeTimeScope.ResolveComponent(new TypedService(type));
        }

        #endregion
    }
}