using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xunit;

namespace SimpleIntegration.Test
{
    public class MessageControllerFact: TestBase
    {

        [Fact]
        public async void should_get_message()
        {
            HttpResponseMessage response = await Client.GetAsync("http://baidu.com/message");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();

            var payload = JsonConvert.DeserializeAnonymousType(content, new {message = default(string)});

            Assert.Equal("Hello Shengqi", payload.message);
        }
    }
}
