using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace SimpleIntegration
{
    public class MessageController: ApiController
    {
        readonly UserInfo userInfo;

        public MessageController(UserInfo userInfo)
        {
            this.userInfo = userInfo;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {message = $"Hello {userInfo.UserName}"});
        }
        [HttpGet]
        public HttpResponseMessage Hello()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {message = "Hello world"});
        }
    }
}