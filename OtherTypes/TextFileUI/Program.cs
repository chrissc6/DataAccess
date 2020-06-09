﻿using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TextFileUI
{
    class Program
    {
        private static IConfiguration _config;
        private static string txtFile;
        private static TextFileDataAccess db = new TextFileDataAccess();

        static void Main(string[] args)
        {
            InitializeConfiguration();
            txtFile = _config.GetValue<string>("TextFile");

            ContactModel user1 = new ContactModel();
            user1.FirstName = "fName1";
            user1.LastName = "lName1";
            user1.EmailAddresses.Add("emailA1@mail.com");
            user1.EmailAddresses.Add("emailA2@mail.com");
            user1.PhoneNumbers.Add("555-111-0001");
            user1.PhoneNumbers.Add("555-111-0002");

            ContactModel user2 = new ContactModel();
            user2.FirstName = "fName2";
            user2.LastName = "lName2";
            user2.EmailAddresses.Add("emailB1@mail.com");
            user2.EmailAddresses.Add("emailB2@mail.com");
            user2.PhoneNumbers.Add("555-222-0001");
            user2.PhoneNumbers.Add("555-222-0002");

            List<ContactModel> contacts = new List<ContactModel>
            {
                user1,
                user2
            };

            Console.WriteLine("Hello World!");
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }
    }
}
