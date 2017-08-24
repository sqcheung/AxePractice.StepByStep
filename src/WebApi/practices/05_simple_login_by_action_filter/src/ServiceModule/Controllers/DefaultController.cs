using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SessionModuleClient;

namespace ServiceModule.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        [LoginRequired]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            #region Please implement the following code

            UserSessionDto userSessionDto = Request.GetUserSession();
            // This method will create response based on current logged in user.
            response.Content = new StringContent($"<h1>This is our awesome API about page for {userSessionDto.UserFullname}</h1>");
            #endregion

            return response;
        }
    }
}