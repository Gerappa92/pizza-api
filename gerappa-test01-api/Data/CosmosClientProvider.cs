using Microsoft.Azure.Cosmos;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerappa_test01_api.Data
{
    public class CosmosClientProvider : ICosmosClientProvider
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;

        public CosmosClientProvider(CosmosClient cosmosClient, string databaseName)
        {
            _cosmosClient = cosmosClient;
            _databaseName = databaseName;
        }
        public CosmosClient GetClient() => _cosmosClient;

        public string GetDatabaseName() => _databaseName;
    }
}
