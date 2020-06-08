using gerappa_test01_api.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerappa_test01_api.Services
{
    public interface ICosmosDbService
    {
        Task AddItemAsync(Pizza item);
        Task DeleteItemAsync(string id);
        Task<Pizza> GetItemAsync(string id);
        Task<IEnumerable<Pizza>> GetItemsAsync(string queryString);
        Task UpdateItemAsync(string id, Pizza item);
    }

    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Pizza item)
        {
            await this._container.CreateItemAsync<Pizza>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Pizza>(id, new PartitionKey(id));
        }

        public async Task<Pizza> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Pizza> response = await this._container.ReadItemAsync<Pizza>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Pizza>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Pizza>(new QueryDefinition(queryString));
            List<Pizza> results = new List<Pizza>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Pizza item)
        {
            await this._container.UpsertItemAsync<Pizza>(item, new PartitionKey(id));
        }
    }
}
