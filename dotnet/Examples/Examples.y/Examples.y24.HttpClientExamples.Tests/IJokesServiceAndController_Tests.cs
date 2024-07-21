using System.Net.Http.Json;
using Examples.y24.Common.Dtos.Joke;
using Examples.y24.HttpClientExamples.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Examples.y24.HttpClientExamples.Tests
{
    [TestClass]
    public class IJokesServiceAndController_Tests
    {
        private WebApplicationFactory<Program> _appFactory = default!;

        [TestInitialize]
        public void Initialize()
        {
            _appFactory = new WebApplicationFactory<Program>();
            //_appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(host =>
            //{
            //    host
            //        .UseEnvironment("Development")
            //        .ConfigureAppConfiguration((context, config) =>
            //        {
            //            config.AddJsonFile("appsettings.json");
            //        })
            //        .ConfigureServices((ctx, services) =>
            //        {
            //            var configValueExample = ctx.Configuration.GetSection("TestKey").Value;
            //            services.AddSingleton<IJokesService, JokesService>();
            //        });
            //});
        }

        [TestCleanup]
        public void Cleanup()
        {
            _appFactory.Dispose();
        }

        [TestMethod]
        public async Task TestControllerAsync()
        {
            var client = _appFactory.CreateClient();
            var response = await client.GetAsync("api/Jokes/GetRandomJoke1");
            var joke = await response.Content.ReadFromJsonAsync<Joke>();
            Assert.IsTrue(joke?.value.Length > 0);
        }

        [TestMethod]
        public async Task TestIServiceAsync()
        {
            var service = _appFactory.Services.GetRequiredService<IJokesService>();
            var joke = await service.GetRandomJoke1Async();
            Assert.IsTrue(joke.value.Length > 0);
        }
    }
}