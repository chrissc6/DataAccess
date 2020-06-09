using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CosmosDBUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("done cosmos");
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
