using ContactManagerAPI.Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: WebJobsStartup(typeof(Startup))]
namespace ContactManagerAPI.Helpers
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFilter(level => true);
            });

            var config = (IConfiguration)builder.Services.First(d => d.ServiceType == typeof(IConfiguration)).ImplementationInstance;

            builder.Services.AddSingleton((s) => 
            {
                CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder(config[Settings.COSMOS_DB_CONNECTION_STRING]);

                return cosmosClientBuilder.WithConnectionModeDirect()
                    .Build();
            });
        }
    }
}
