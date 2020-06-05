using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace SQLServerUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //test connstring
            //Console.WriteLine(GetConnectionString());

            //instance of sqlcrud class, and pass in connstring
            SqlCrud sql = new SqlCrud(GetConnectionString());

            //ReadAllContacts(sql);

            ReadContact(sql, 1);

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

        private static void ReadAllContacts(SqlCrud sql)
        {
            //calls sqlcrud to get all contacts
            var rows = sql.GetAllContacts();

            foreach (var i in rows)
            {
                Console.WriteLine($"Id: {i.Id}, FirstName: {i.FirstName}, LastName: {i.LastName}");
            }
        }

        private static void ReadContact(SqlCrud sql, int id)
        {
            var contact = sql.GetFullContactById(id);

            Console.WriteLine($"Id: {contact.BasicInfo.Id}, FirstName: {contact.BasicInfo.FirstName}, LastName: {contact.BasicInfo.LastName}");
        }
    }
}
