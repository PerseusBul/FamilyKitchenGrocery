namespace FamilyKitchen.Web.Tests
{
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Testing;

    using Xunit;

    public class WebTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public async Task IndexPageShouldHaveBodyTag()
        {
            var serverFactory = new WebApplicationFactory<Startup>();
            var client = serverFactory.CreateClient();

            var response = await client.GetAsync("/");
            var responseAsString = await response.Content.ReadAsStringAsync();
            Assert.Contains("<body class=\"goto-here\">", responseAsString);
        }

        [Fact]
        public async Task AboutPageShouldReturnStatusCodeOK()
        {
            var serverFactory = new WebApplicationFactory<Startup>();
            var client = serverFactory.CreateClient();

            var response = await client.GetAsync("/Home/About");

            Assert.Equal(HttpStatusCode.OK, response.EnsureSuccessStatusCode().StatusCode);
        }

        [Fact]
        public async Task HomePageResponseShouldHaveCacheControlHeader()
        {
            var serverFactory = new WebApplicationFactory<Startup>();
            var client = serverFactory.CreateClient();

            var response = await client.GetAsync("/");

            Assert.True(response.Headers.Contains("Cache-Control"));
        }

        [Fact]
        public async Task HomePageRequestUriShouldBeLocalhost()
        {
            var serverFactory = new WebApplicationFactory<Startup>();
            var client = serverFactory.CreateClient();

            var response = await client.GetAsync("/");

            var res = response.RequestMessage.Properties.ToString();
            Assert.Equal("http://localhost/", response.RequestMessage.RequestUri.ToString());
        }

    }
}
