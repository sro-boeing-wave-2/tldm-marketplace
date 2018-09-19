using System.Text;
using MarketPlaceBackend.Models;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarketPlaceBackend.Tests
{
    public class MarketPlaceIntegrationTest
    {
        private HttpClient _client;
        public MarketPlaceIntegrationTest()
        {
            var host = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>());
            _client = host.CreateClient();
        }

        private async Task CreateTestDataAsync()
        {
            var app1 = new Application()
            {
                Id = "abcd-efgh",
                Name = "Github",
                Info = "Github Integration",
                AppUrl = "www.github.com",
                Developer = "Mr. XYZ",
                LogoUrl = "www.logo.com"
            };
            var app2 = new Application()
            {
                Id = "wxyz-abcd",
                Name = "Google Drive",
                Info = "Google Drive Integration",
                AppUrl = "www.googledrive.com",
                Developer = "Mr. ABC",
                LogoUrl = "www.glogo.com"
            };
            var json = JsonConvert.SerializeObject(app1);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var Response = await _client.PostAsync("/api/applications", stringContent);

            json = JsonConvert.SerializeObject(app2);
            stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            Response = await _client.PostAsync("/api/applications", stringContent);
        }

        // [Fact]
        // public async Task TestGetRequestAsync()
        // {
        //     var Response = await _client.GetAsync("/api/applications");
        //     var ResponseBody = await Response.Content.ReadAsStringAsync();
        //     Response.EnsureSuccessStatusCode();
        // }

        // [Fact]
        // public async Task TestGetByIdForNullCase()
        // {
        //     var Response = await _client.GetAsync("/api/applications/abcd-efgh");
        //     Assert.Equal("NotFound", Response.StatusCode.ToString());
        // }

        // [Fact]
        // public async Task TestGetById()
        // {
        //     await CreateTestDataAsync();
        //     var Response = await _client.GetAsync("/api/applications/abcd-efgh");
        //     var ApplicationAsString = await Response.Content.ReadAsStringAsync();
        //     JObject applicationObject = JObject.Parse(ApplicationAsString);

        //     Assert.Equal("Github", applicationObject.GetValue("name"));
        //     Assert.Equal("Github Integration", applicationObject.GetValue("info"));
        //     Assert.Equal("Mr. XYZ", applicationObject.GetValue("developer"));
        //     Assert.Equal("www.github.com", applicationObject.GetValue("appUrl"));
        //     Assert.Equal("www.logo.com", applicationObject.GetValue("logoUrl"));

        //     Response.EnsureSuccessStatusCode();
        // }

        // [Fact]
        // public async Task TestForDeleteAsync()
        // {
        //     await CreateTestDataAsync();

        //     var Response = await _client.DeleteAsync("/api/applications/abcd-efgh");
        //     var ApplicationAsString = await Response.Content.ReadAsStringAsync();
        //     JObject applicationObject = JObject.Parse(ApplicationAsString);

        //     Assert.Equal("Github", applicationObject.GetValue("name"));
        //     Assert.Equal("Github Integration", applicationObject.GetValue("info"));
        //     Assert.Equal("Mr. XYZ", applicationObject.GetValue("developer"));
        //     Assert.Equal("www.github.com", applicationObject.GetValue("appUrl"));
        //     Assert.Equal("www.logo.com", applicationObject.GetValue("logoUrl"));

        //     Response.EnsureSuccessStatusCode();
        // }

        // [Fact]
        // public async Task TestForPutAsync()
        // {
        //     await CreateTestDataAsync();
        //     var App = new Application()
        //     {
        //         Id = "abcd-efgh",
        //         Name = "Github Updated"
        //     };
        //     var json = JsonConvert.SerializeObject(App);
        //     var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
        //     var Response = await _client.PutAsync("/api/applications/abcd-efgh", stringContent);

        //     Assert.Equal("NoContent", Response.StatusCode.ToString());

        //     Response = await _client.DeleteAsync("/api/applications/abcd-efgh");
        //     var ApplicationAsString = await Response.Content.ReadAsStringAsync();
        //     JObject applicationObject = JObject.Parse(ApplicationAsString);

        //     Assert.Equal("Github Updated", applicationObject.GetValue("name"));
        //     Assert.Equal("Github Integration", applicationObject.GetValue("info"));
        //     Assert.Equal("Mr. XYZ", applicationObject.GetValue("developer"));
        //     Assert.Equal("www.github.com", applicationObject.GetValue("appUrl"));
        //     Assert.Equal("www.logo.com", applicationObject.GetValue("logoUrl"));

        //     Response.EnsureSuccessStatusCode();
        // }

        // [Fact]
        // public async Task TestForPostAsync()
        // {
        //     var app = new Application()
        //     {
        //         Id = "klmo-pqrs",
        //         Name = "Twitter",
        //         Info = "Twitter Integration",
        //         AppUrl = "www.twitter.com",
        //         Developer = "Mr. Twit",
        //         LogoUrl = "www.twitterlogo.com"
        //     };
        //     var json = JsonConvert.SerializeObject(app);
        //     var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
        //     var Response = await _client.PostAsync("/api/applications", stringContent);
        //     Assert.Equal("Created", Response.StatusCode.ToString());
        // }

        //[Fact]
        //public async Task TestForListOfApplicationsWithoutId()
        //{
        //    await CreateTestDataAsync();
        //    var Response = await _client.GetAsync("/api/applications/all");
        //    var JSONAsString = await Response.Content.ReadAsStringAsync();
        //    var AppsWithoutIdArray = JArray.Parse(JSONAsString);
        //    JToken App1 = AppsWithoutIdArray[0];
        //    JToken App2 = AppsWithoutIdArray[1];
        //    Assert.Null(App1["id"]);
        //    Assert.Equal("Github", App1["name"]);
        //    Assert.Equal("Github Integration", App1["info"]);
        //    Assert.Equal("Mr. XYZ", App1["developer"]);
        //    Assert.Equal("www.github.com", App1["appUrl"]);
        //    Assert.Equal("www.logo.com", App1["logoUrl"]);

        //    Assert.Null(App2["id"]);
        //    Assert.Equal("Google Drive", App2["name"]);
        //    Assert.Equal("Google Drive Integration", App2["info"]);
        //    Assert.Equal("Mr. ABC", App2["developer"]);
        //    Assert.Equal("www.googledrive.com", App2["appUrl"]);
        //    Assert.Equal("www.glogo.com", App2["logoUrl"]);
        //}

    }
}
