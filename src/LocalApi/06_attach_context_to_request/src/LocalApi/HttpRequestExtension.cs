using System;
using System.Net.Http;
using LocalApi.Routing;

namespace LocalApi
{
    static class HttpRequestExtension
    {
        const string requestContextKey = "LocalApi.RequestContext";

        #region Please implement these 2 methods to pass all the tests

        /*
         * You should create a request context to hold all the readonly information
         * needed for processing a request. The HttpRequestMessage has a greate property
         * called Properties to hold additional information, which will help you. You
         * should use requestContextKey to store and retrive the request context.
         */

        public static void SetRequestContext(
            this HttpRequestMessage request,
            HttpConfiguration configuration,
            HttpRoute matchedRoute)
        {
            if (matchedRoute == null){throw new ArgumentNullException(nameof(matchedRoute));}
            if (configuration == null){throw new ArgumentNullException(nameof(configuration));}
            request.Properties.Add(requestContextKey, new HttpRequestContext(configuration, matchedRoute));
        }

        public static HttpRequestContext GetRequestContext(this HttpRequestMessage request)
        {
            object context;
            if (!request.Properties.TryGetValue(requestContextKey, out context))
            {
                return null;
            }
            return (HttpRequestContext) context;
        }

        #endregion
    }
}