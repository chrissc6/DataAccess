using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CosmosDBUI
{
    class Program
    {
        private static CosmosDBDataAccess db;

        static async Task Main(string[] args)
        {
            var ci = GetCosmosInfo();
            db = new CosmosDBDataAccess(ci.endpointUrl, ci.primaryKey, ci.dbName, ci.containerName);

            ContactModel nc = new ContactModel
            {
                FirstName = "nFname1",
                LastName = "nLname1"
            };
            nc.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "ncEmail1@mail.com" });
            nc.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "ncEmail2@mail.com" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-123-1111" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-123-2222" });

            ContactModel nc2 = new ContactModel
            {
                FirstName = "nFname2",
                LastName = "nLname2"
            };
            nc2.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "nc2Email1@mail.com" });
            nc2.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "nc2Email2@mail.com" });
            nc2.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-456-1111" });
            nc2.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-456-2222" });

            await CreateContactAsync(nc);
            await CreateContactAsync(nc2);

            await GetAllContactsAsync();

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

        private static async Task CreateContactAsync(ContactModel contact)
        {
            await db.UpsertRecordAsync(contact);
        }

        private static async Task GetAllContactsAsync()
        {
            var contacts = await db.LoadRecordsAsync<ContactModel>();

            foreach (var i in contacts)
            {
                Console.WriteLine($"Id: {i.Id}, FirstName: {i.FirstName}, LastName: {i.LastName}");
            }
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
