using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Standards.AspNetCore.Tests.TestServer;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Standards.AspNetCore.Tests
{
    public class IsoDateModelBinderTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IsoDateModelBinderTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("2021-08-21", "attribute", HttpStatusCode.OK)]
        [InlineData("", "attribute", HttpStatusCode.BadRequest)]        
        [InlineData("2021-21-08", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("08-21-2021", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("21-08-2021", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("2021/08/21", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("2021/21/08", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("08/21/2021", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("21/08/2021", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("08.21.2021", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("21.08.2021", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("2021.08.21", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("2021.21.08", "attribute", HttpStatusCode.BadRequest)]
        [InlineData("2021-08-21", "provider", HttpStatusCode.OK)]
        [InlineData("", "provider", HttpStatusCode.BadRequest)]
        [InlineData("2021-21-08", "provider", HttpStatusCode.BadRequest)]
        [InlineData("08-21-2021", "provider", HttpStatusCode.BadRequest)]
        [InlineData("21-08-2021", "provider", HttpStatusCode.BadRequest)]
        [InlineData("2021/08/21", "provider", HttpStatusCode.BadRequest)]
        [InlineData("2021/21/08", "provider", HttpStatusCode.BadRequest)]
        [InlineData("08/21/2021", "provider", HttpStatusCode.BadRequest)]
        [InlineData("21/08/2021", "provider", HttpStatusCode.BadRequest)]
        [InlineData("08.21.2021", "provider", HttpStatusCode.BadRequest)]
        [InlineData("21.08.2021", "provider", HttpStatusCode.BadRequest)]
        [InlineData("2021.08.21", "provider", HttpStatusCode.BadRequest)]
        [InlineData("2021.21.08", "provider", HttpStatusCode.BadRequest)]
        public async Task AttributeTests(string date, string route, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            using var response = await client.GetAsync($"/IsoDateModelBinder/{route}?date={date}");

            // Assert
            if (expectedStatusCode != HttpStatusCode.OK)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var problem = await JsonSerializer.DeserializeAsync<ProblemDetails>(responseStream);
                var error = JsonSerializer.Deserialize<Error>(problem.Extensions["errors"].ToString());
                Assert.Equal("Invalid date; must be in ISO-8601 format i.e. YYYY-MM-DD.", error.Date[0]);
            }
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }        
    }

    public class Error
    {
        [JsonPropertyName("date")]
        public string[] Date { get; set; }
    }
}