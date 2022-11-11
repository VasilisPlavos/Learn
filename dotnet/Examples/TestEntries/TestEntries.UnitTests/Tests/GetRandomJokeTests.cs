using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestEntries.Api.Controllers;
using TestEntries.Common.Contracts;
using TestEntries.Common.Services;
using TestEntries.Contracts.Contracts.Joke;

namespace TestEntries.UnitTests.Tests
{
    public class GetRandomJokeTests
    {
        private static readonly ContractAssert assert = new();

        [Fact]
        public async Task BaseTests()
        {
            var controller = new TestEntriesController();

            var payload = new JokeRequestPackage()
            {
                EnvHeader = "null",
                LogHeader = "null",
                ApprovalHeader = "null",
                RequestData = new JokeRequestContract()
            };
            var responsePackage = await controller.GetRandomJoke(payload) as JsonResult;
            var jsonResponse = JsonConvert.SerializeObject(responsePackage);
            assert.AssertSuccess(responsePackage.Value as JokeResponsePackage);
        }

    }
}
