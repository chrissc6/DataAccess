using DataAccessLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MongoDBUI
{
    class Program
    {
        private static MongoDBDataAccess db;

        static void Main(string[] args)
        {
            db = new MongoDBDataAccess("MongoContactsDB", GetConnectionString());
            Console.WriteLine("done mongo");
            Console.ReadLine();
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
