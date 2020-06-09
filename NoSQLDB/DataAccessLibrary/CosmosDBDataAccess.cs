using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
