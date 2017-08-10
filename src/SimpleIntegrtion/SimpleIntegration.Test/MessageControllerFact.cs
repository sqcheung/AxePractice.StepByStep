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
            var fakeLogger = (FakeMyLogger)container.Resolve<IMyLogger>();

            HttpResponseMessage response = await Client.GetAsync("http://baidu.com/message");

            string content = await response.Content.ReadAsStringAsync();
            var payload = JsonConvert.DeserializeAnonymousType(content, new {message = default(string)});

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Hello Shengqi", payload.message);

            var logs = fakeLogger.GetLogs();
            Assert.Equal(2, logs.Count);
            Assert.Contains("Request beginning....", logs[0]);
            Assert.Contains("cycle time", logs[1]);
        }
    }
}
