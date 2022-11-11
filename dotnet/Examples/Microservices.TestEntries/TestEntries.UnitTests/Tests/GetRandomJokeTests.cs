using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using TestEntries.Api.Controllers;
using TestEntries.Common.Services;
using TestEntries.Contracts.Contracts.Joke;

namespace TestEntries.UnitTests.Tests
{
    public class GetRandomJokeTests
    {
        private static readonly ContractAssert Assert = new();

        [Fact]
        public async Task BaseTests()
        {
            var controller = new TestEntriesController();

            var requestData = new JokeRequestPackage()
            {
                EnvHeader = "null",
                LogHeader = "null",
                ApprovalHeader = "null",
                RequestData = new JokeRequestContract()
            };

            var responsePackage = await controller.GetRandomJoke(requestData) as JsonResult;
            var jsonResponse = JsonConvert.SerializeObject(responsePackage);
            Assert.AssertSuccess(responsePackage.Value as JokeResponsePackage);
        }

    }
}
