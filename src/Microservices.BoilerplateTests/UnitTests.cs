using Divergic.Logging.Xunit;
using Microservices.Boilerplate;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microservices.BoilerplateTests
{
    public class UnitTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly TestRequestService _requestService;
        private readonly CustomWebApplicationFactory<Startup> _webApplicationFactory;

        public UnitTests(
            ITestOutputHelper outputHelper,
            CustomWebApplicationFactory<Startup> factory)
        {
            _webApplicationFactory = factory;

            _loggerFactory = LogFactory.Create(outputHelper);
            _loggerFactory.AddProvider(new TestOutputLoggerProvider(outputHelper));

            var httpClient = factory.CreateClient();
            _requestService = new TestRequestService(httpClient, _loggerFactory);
        }

        [Fact]
        public async Task Post_WithNullModel_ReturnsBadRequest()
        {
            await _requestService.PostAsync("/v1/test", default(TestModel), handleResponse: (response) =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                return true;
            });
        }

        [Fact]
        public async Task Post_WithInvalidModel_ReturnsOk()
        {
            var model = new TestModel
            {
                RequiredField = null,
                OptionalField = $"{Guid.NewGuid()}"
            };

            await _requestService.PostAsync("/v1/test", model, handleResponse: (response) =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                return true;
            });
        }

        [Fact]
        public async Task Post_WithCorrectModel_ReturnsOk()
        {
            var model = new TestModel
            {
                RequiredField = $"{Guid.NewGuid()}",
                OptionalField = $"{Guid.NewGuid()}"
            };

            await _requestService.PostAsync("/v1/test", model, handleResponse: (response) =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                return true;
            });
        }

        [Fact]
        public async Task Get_ReturnsMicroserviceError()
        {
            await _requestService.GetAsync<MicroserviceError>("/v1/test", handleResponse: (response) =>
            {
                Assert.False(response.IsSuccessStatusCode);

                var json = response.Content.ReadAsStringAsync().Result;
                var error = JsonSerializer.Deserialize<MicroserviceError>(json);
                Assert.NotNull(error);
                Assert.Equal(500, error.StatusCode);

                return true;
            });
        }
    }
}
