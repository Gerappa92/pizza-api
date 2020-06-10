using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gerappa_test01_api.Data
{
    public interface ICosmosClientProvider
    {
        public CosmosClient GetClient();
        public string GetDatabaseName();

    }
}
