using System.Threading.Tasks;
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
    public class Iso8601DateTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Iso8601DateTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("2021-08-21", HttpStatusCode.OK)]
        [InlineData("", HttpStatusCode.BadRequest)]
        [InlineData("2021-21-08", HttpStatusCode.BadRequest)]
        [InlineData("08-21-2021", HttpStatusCode.BadRequest)]
        [InlineData("21-08-2021", HttpStatusCode.BadRequest)]
        [InlineData("2021/08/21", HttpStatusCode.BadRequest)]
        [InlineData("2021/21/08", HttpStatusCode.BadRequest)]
        [InlineData("08/21/2021", HttpStatusCode.BadRequest)]
        [InlineData("21/08/2021", HttpStatusCode.BadRequest)]
        [InlineData("08.21.2021", HttpStatusCode.BadRequest)]
        [InlineData("21.08.2021", HttpStatusCode.BadRequest)]
        [InlineData("2021.08.21", HttpStatusCode.BadRequest)]
        [InlineData("2021.21.08", HttpStatusCode.BadRequest)]
        public async Task AttributeTests(string date, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/iso8601date/attribute?date={date}");

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory]
        [InlineData("2021-08-21", HttpStatusCode.OK)]
        [InlineData("", HttpStatusCode.BadRequest)]
        [InlineData("2021-21-08", HttpStatusCode.BadRequest)]
        [InlineData("08-21-2021", HttpStatusCode.BadRequest)]
        [InlineData("21-08-2021", HttpStatusCode.BadRequest)]
        [InlineData("2021/08/21", HttpStatusCode.BadRequest)]
        [InlineData("2021/21/08", HttpStatusCode.BadRequest)]
        [InlineData("08/21/2021", HttpStatusCode.BadRequest)]
        [InlineData("21/08/2021", HttpStatusCode.BadRequest)]
        [InlineData("08.21.2021", HttpStatusCode.BadRequest)]
        [InlineData("21.08.2021", HttpStatusCode.BadRequest)]
        [InlineData("2021.08.21", HttpStatusCode.BadRequest)]
        [InlineData("2021.21.08", HttpStatusCode.BadRequest)]
        public async Task ProviderTests(string date, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/iso8601date/provider?date={date}");

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }        
    }
}