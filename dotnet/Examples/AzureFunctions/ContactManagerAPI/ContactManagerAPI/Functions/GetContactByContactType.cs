using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using ContactManagerAPI.Helpers;
using ContactManagerAPI.Models;

namespace ContactManagerAPI.Functions
{
    public class GetContactByContactType
    {
        private readonly ILogger _logger;
        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _config;

        private Database _contactDatabase;
        private Container _contactContainer;

        public GetContactByContactType(
            ILogger<GetContactByContactType> logger,
            CosmosClient cosmosClient,
            IConfiguration config)
        {
            _logger = logger;
            _cosmosClient = cosmosClient;
            _config = config;

            _contactDatabase = _cosmosClient.GetDatabase(_config[Settings.DATABASE_NAME]);
            _contactContainer = _contactDatabase.GetContainer(_config[Settings.CONTAINER_NAME]);
        }

        [FunctionName(nameof(GetContactByContactType))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contacts/{contactType}")] HttpRequest req,
            string contactType)
        {
            IActionResult returnValue = null;

            try
            {
                QueryDefinition query = new QueryDefinition(
                    $"SELECT * FROM {_contactContainer.Id} c WHERE c.ContactType = @ContactType")
                    .WithParameter("@ContactType", contactType);

                FeedIterator<Contact> resultSet = _contactContainer.GetItemQueryIterator<Contact>(
                    query,
                    requestOptions: new QueryRequestOptions()
                    {
                        PartitionKey = new PartitionKey(contactType),
                        MaxItemCount = 10
                    });

                while (resultSet.HasMoreResults)
                {
                    _logger.LogInformation($"Getting all contacts of {contactType} contact type");
                    FeedResponse<Contact> response = await resultSet.ReadNextAsync();
                    returnValue = new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't find all contacts of type {contactType}. Exception thrown: {ex.Message}");
                returnValue = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnValue;
        }
    }
}
