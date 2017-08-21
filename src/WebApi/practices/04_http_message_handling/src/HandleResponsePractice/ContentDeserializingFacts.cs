using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace HandleResponsePractice
{
    public class ContentDeserializingFacts
    {
        [Fact]
        public async Task should_deserialize_json_content()
        {
            HttpResponseMessage response = await ClientHelper.Client.GetAsync("complex");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            object content = null;

            #region Please modifies the following code to pass the test

            // I just want { id, sizes } here. Please deserialize the content. You cannot
            // change any code beyond the region.
            string contentString = await response.Content.ReadAsStringAsync();
            content = JsonConvert.DeserializeAnonymousType(contentString, new { id = default(int), sizes = default(IEnumerable<string>) });
            #endregion

            Assert.Equal(2, content.GetPublicDeclaredProperties().Length);
            Assert.Equal(1, content.GetPropertyValue<int>("id"));
            Assert.Equal(new [] { "Large", "Medium", "Small" }, content.GetPropertyValue<IEnumerable<string>>("sizes"));
        }

        [Fact]
        public async Task should_get_properties_from_deserialized_content()
        {
            HttpResponseMessage response = await ClientHelper.Client.GetAsync("complex");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            object content = await response.Content.ReadAsAsync<object>();

            int id = default(int);
            string name = default(string);
            IEnumerable<string> sizes = default(IEnumerable<string>);

            #region Please modifies the following code to pass the test

            // I want { id, name, sizes } here. Please get properties from the content. 
            // You cannot change any code beyond the region.
            var jObject = content as JObject;
            id = jObject["id"].ToObject<int>();
            name = jObject["name"].ToObject<string>();
            sizes = jObject["sizes"].ToObject<IEnumerable<string>>();

            #endregion
            
            Assert.Equal(1, id);
            Assert.Equal("Apple", name);
            Assert.Equal(new[] { "Large", "Medium", "Small" }, sizes);
        }
    }
}