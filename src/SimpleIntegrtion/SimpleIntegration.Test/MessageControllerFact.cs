using System.Net;
using System.Net.Http;
using Autofac;
using Newtonsoft.Json;
using Xunit;

namespace SimpleIntegration.Test
{
    public class MessageControllerFact: TestBase
    {
        [Fact]
        public async void should_get_message()
        {
            var fakeLogger = buildContainer.Resolve<IMyLogger>();
            HttpResponseMessage response = await Client.GetAsync("http://baidu.com/message");

            string content = await response.Content.ReadAsStringAsync();
            var payload = JsonConvert.DeserializeAnonymousType(content, new {message = default(string)});

            var logs = ((FakeMyLogger) fakeLogger).GetLogs();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Hello Shengqi", payload.message);

            Assert.Equal(2, logs.Count);
        }

        [Fact]
        public void get_life_time_for_each_request()
        {

        }
    }
}
