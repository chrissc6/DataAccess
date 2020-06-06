using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MySQLUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //test connstring
            //Console.WriteLine(GetConnectionString());

            //instance of sqlcrud class, and pass in connstring
            MySQLCrud sql = new MySQLCrud(GetConnectionString());

            //ReadAllContacts(sql);
            //CreateNewContact(sql);
            //ReadContact(sql, 2);
            //UpdateContact(sql);
            //RemovePhoneNumberFromContact(sql, 1, 1);

            Console.WriteLine("done mysql");
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

        private static void ReadAllContacts(MySQLCrud sql)
        {
            //calls sqlcrud to get all contacts
            var rows = sql.GetAllContacts();

            foreach (var i in rows)
            {
                Console.WriteLine($"Id: {i.Id}, FirstName: {i.FirstName}, LastName: {i.LastName}");
            }
        }

        private static void CreateNewContact(MySQLCrud sql)
        {
            FullContactModel nc = new FullContactModel
            {
                BasicInfo = new BasicContactModel
                {
                    FirstName = "ncFname",
                    LastName = "ncLname"
                }
            };

            nc.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "ncEmail@mail.com" });
            nc.EmailAddresses.Add(new EmailAddressModel { Id = 2, EmailAddress = "test2@mail.com" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "555-213-3333" });
            nc.PhoneNumbers.Add(new PhoneNumberModel { Id = 1, PhoneNumber = "555-123-1111" });

            sql.CreateContact(nc);
        }

        private static void ReadContact(MySQLCrud sql, int id)
        {
            var contact = sql.GetFullContactById(id);

            Console.WriteLine($"Id: {contact.BasicInfo.Id}, FirstName: {contact.BasicInfo.FirstName}, LastName: {contact.BasicInfo.LastName}");
        }

        private static void UpdateContact(MySQLCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "fName1z",
                LastName = "lName1"
            };
            sql.UpdateContactName(contact);
        }

        private static void RemovePhoneNumberFromContact(MySQLCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumber(contactId, phoneNumberId);
        }
    }
}
