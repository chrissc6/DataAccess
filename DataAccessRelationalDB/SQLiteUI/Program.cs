using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SQLiteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //test connstring
            //Console.WriteLine(GetConnectionString());

            //instance of sqlcrud class, and pass in connstring
            //SqlCrud sql = new SqlCrud(GetConnectionString());

            //ReadAllContacts(sql);
            //ReadContact(sql, 3);
            //CreateNewContact(sql);
            //UpdateContact(sql);
            //RemovePhoneNumberFromContact(sql, 1, 1);

            Console.WriteLine("done");
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
