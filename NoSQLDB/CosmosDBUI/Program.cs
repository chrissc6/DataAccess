using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CosmosDBUI
{
    class Program
    {
        private static CosmosDBDataAccess db;
        static void Main(string[] args)
        {
            var ci = GetCosmosInfo();
            db = new CosmosDBDataAccess(ci.endpointUrl, ci.primaryKey, ci.dbName, ci.containerName);

            Console.WriteLine("done cosmos");
            Console.ReadLine();
        }

        private static (string endpointUrl, string primaryKey, string dbName, string containerName) GetCosmosInfo()
        {
            (string endpointUrl, string primaryKey, string dbName, string containerName) output;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output.endpointUrl = config.GetValue<string>("CosmosDB:EndpointUrl");
            output.primaryKey = config.GetValue<string>("CosmosDB:PrimaryKey");
            output.dbName = config.GetValue<string>("CosmosDB:DatabaseName");
            output.containerName = config.GetValue<string>("CosmosDB:ContainerName");

            return output;
        }

        private static void CreateContact(ContactModel contact)
        {
        }

        private static void GetAllContacts()
        {
        }

        private static void GetContactById(string id)
        {
        }

        private static void UpdateContactsFirstName(string firstName, string id)
        {
        }

        public static void RemovePhoneNumber(string phoneNumber, string id)
        {
        }

        public static void RemoveContact(string id)
        {
        }
    }
}
