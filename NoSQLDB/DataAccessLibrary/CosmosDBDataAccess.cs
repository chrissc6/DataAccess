using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    //one container (table) per instance
    public class CosmosDBDataAccess
    {
        private readonly string endPointUrl;
        private readonly string primaryKey;
        private readonly string dbName;
        private readonly string containerName;
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;

        public CosmosDBDataAccess(string endPointUrl, string primaryKey, string dbName, string containerName)
        {
            this.endPointUrl = endPointUrl;
            this.primaryKey = primaryKey;
            this.dbName = dbName;
            this.containerName = containerName;

            //establishes connection to azure
            //endPointUrl is url of database
            //primaryKey allows you to unlock the server
            cosmosClient = new CosmosClient(endPointUrl, primaryKey);

            //connect to a database
            //database has one or more containers
            database = cosmosClient.GetDatabase(dbName);

            //connect to a container
            //container are like tables
            container = database.GetContainer(containerName);
        }

        public async Task<List<T>> LoadRecordsAsync<T>()
        {
            string sql = "SELECT * FROM c";

            QueryDefinition queryDefinition = new QueryDefinition(sql);
            FeedIterator<T> feedIterator = container.GetItemQueryIterator<T>(queryDefinition);

            List<T> output = new List<T>();

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await feedIterator.ReadNextAsync();

                foreach (var i in currentResultSet)
                {
                    output.Add(i);
                }
            }

            return output;
        }

        public async Task<T> LoadRecordByIdAsync<T>(string id)
        {
            string sql = "SELECT * FROM c WHERE c.id = @Id";

            QueryDefinition queryDefinition = new QueryDefinition(sql).WithParameter("@Id", id);
            FeedIterator<T> feedIterator = container.GetItemQueryIterator<T>(queryDefinition);

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await feedIterator.ReadNextAsync();

                foreach (var i in currentResultSet)
                {
                    return i;
                }
            }

            //or return a null
            throw new Exception("Item not found");
        }

        public async Task UpsertRecordAsync<T>(T record)
        {
            await container.UpsertItemAsync(record);
        }
    }
}
