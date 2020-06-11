using gerappa_test01_api.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<T>> GetAll()
        {
            var items = this._container.GetItemLinqQueryable<T>().ToFeedIterator();
            return await GetListAsync(items);
        }

        public async Task Update(T entity)
        {
            await this._container.UpsertItemAsync<T>(entity, new PartitionKey(entity.Id));
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression)
        {
            var items = this._container.GetItemLinqQueryable<T>().Where(expression).ToFeedIterator();
            return await GetListAsync(items);

        }

        private async Task<IEnumerable<T>> GetListAsync(FeedIterator<T> items)
        {
            List<T> result = new List<T>();
            while (items.HasMoreResults)
            {
                foreach (var item in await items.ReadNextAsync())
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
