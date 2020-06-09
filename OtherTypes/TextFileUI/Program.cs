using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;

namespace TextFileUI
{
    class Program
    {
        private static IConfiguration _config;

        static void Main(string[] args)
        {
            InitializeConfiguration();

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
