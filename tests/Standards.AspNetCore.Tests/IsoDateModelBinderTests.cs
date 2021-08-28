using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Standards.AspNetCore.Tests.TestServer;

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
            var response = await client.GetAsync($"/IsoDateModelBinder/{route}?date={date}");

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }        
    }
}