using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using Newtonsoft.Json;


namespace SessionModuleClient
{
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public override bool AllowMultiple { get; } = false;

        public override async Task OnActionExecutingAsync(
            HttpActionContext context, 
            CancellationToken cancellationToken)
        {
            #region Please implement the method

            // This filter will try resolve session cookies. If the cookie can be
            // parsed correctly, then it will try calling session API to get the
            // specified session. To ease user session access, it will store the
            // session object in request message properties.
            var cookieValue = context.Request.Headers.GetCookies("X-Session-Token").FirstOrDefault();
            if (cookieValue == null)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }

            var cookie = cookieValue.Cookies.FirstOrDefault(c => c.Name == "X-Session-Token");
            if (cookie == null)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }

            IDependencyScope scope = context.Request.GetDependencyScope();
            var Client = scope.GetService(typeof(HttpClient)) as HttpClient;

            string BaseAddress = new Uri(context.Request.RequestUri, context.Request.GetRequestContext().VirtualPathRoot).AbsoluteUri;
            HttpResponseMessage getSessionResponse = await Client.GetAsync($"{BaseAddress}session/{cookie.Value}");

            if (!getSessionResponse.IsSuccessStatusCode)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }
            string contentString = await getSessionResponse.Content.ReadAsStringAsync();
            var payload = JsonConvert.DeserializeAnonymousType(contentString,
                new {token = default(string), userFullname = default(string)});
            context.Request.SetUserSession(new UserSessionDto() {Token = payload.token, UserFullname = payload.userFullname});

            #endregion
        }
    }
}