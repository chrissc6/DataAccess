using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace MongoDBUI
{
    class Program
    {
        private static MongoDBDataAccess db;
        private static readonly string tableName = "Contacts";

        static void Main(string[] args)
        {
            db = new MongoDBDataAccess("MongoContactsDB", GetConnectionString());

            ContactModel nc = new ContactModel
            {
                FirstName = "nFname1",
                LastName = "nLname1"
            };
            nc.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "ncEmail1@mail.com" });
            nc.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "ncEmail2@mail.com" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-123-1111" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-123-2222" });

            //CreateContact(nc);
            //GetAllContacts();
            //GetContactById("9c8e2f9f-15c1-411e-9246-a99908dcb5b5");
            //UpdateContactsFirstName("nFname1z", "9c8e2f9f-15c1-411e-9246-a99908dcb5b5");
            //RemovePhoneNumber("555-123-2222", "9c8e2f9f-15c1-411e-9246-a99908dcb5b5");
            //RemoveContact("9c8e2f9f-15c1-411e-9246-a99908dcb5b5");

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

        private static void GetAllContacts()
        {
            var contacts = db.LoadRecords<ContactModel>(tableName);

            foreach (var i in contacts)
            {
                Console.WriteLine($"Id: {i.Id}, FirstName: {i.FirstName}, LastName: {i.LastName}");
            }
        }

        private static void GetContactById(string id)
        {
            Guid guid = new Guid(id);
            var contact = db.LoadRecordById<ContactModel>(tableName, guid);

            Console.WriteLine($"Id: {contact.Id}, FirstName: {contact.FirstName}, LastName: {contact.LastName}");
        }

        private static void UpdateContactsFirstName(string firstName, string id)
        {
            Guid guid = new Guid(id);
            var contact = db.LoadRecordById<ContactModel>(tableName, guid);

            contact.FirstName = firstName;

            db.UpsertRecord(tableName, contact.Id, contact);
        }

        public static void RemovePhoneNumber(string phoneNumber, string id)
        {
            Guid guid = new Guid(id);
            var contact = db.LoadRecordById<ContactModel>(tableName, guid);

            contact.PhoneNumbers = contact.PhoneNumbers.Where(x => x.PhoneNumber != phoneNumber).ToList();

            db.UpsertRecord(tableName, contact.Id, contact);
        }

        public static void RemoveContact(string id)
        {
            Guid guid = new Guid(id);
            db.DeleteRecord<ContactModel>(tableName, guid);
        }
    }
}
