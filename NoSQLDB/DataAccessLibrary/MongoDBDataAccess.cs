using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary
{
    public class MongoDBDataAccess
    {
        private IMongoDatabase db;

        public MongoDBDataAccess(string dbName, string connString)
        {
            var client = new MongoClient(connString);
            db = client.GetDatabase(dbName);
        }
    }
}
