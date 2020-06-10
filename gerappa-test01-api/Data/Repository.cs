using gerappa_test01_api.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace gerappa_test01_api.Data
{
    public class Repository<T> : IRepository<T> where T : CosmoEntity
    {
        private Container _container;

        public Repository(ICosmosClientProvider cosmosClientProvider)
        {
            var dbClient = cosmosClientProvider.GetClient();
            try
            {
                _container = dbClient.GetContainer(cosmosClientProvider.GetDatabaseName(), typeof(T).Name.ToLower());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> Add(T entity)
        {
            return await this._container.CreateItemAsync<T>(entity, new PartitionKey(entity.Id));
        }

        public async Task Delete(string id)
        {
            await this._container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task<T> Get(string id)
        {
            try
            {
                ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public Task<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(T entity)
        {
            await this._container.UpsertItemAsync<T>(entity, new PartitionKey(entity.Id));
        }
    }
}
