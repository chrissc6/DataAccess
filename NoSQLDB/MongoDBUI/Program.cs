using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MongoDBUI
{
    class Program
    {
        private static MongoDBDataAccess db;
        private static readonly string tableName = "Contacts";

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

        private static void CreateContact(ContactModel contact)
        {
            db.UpsertRecord(tableName, contact.Id, contact);
        }
    }
}
