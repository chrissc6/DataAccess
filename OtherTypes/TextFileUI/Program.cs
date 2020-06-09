using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;

namespace TextFileUI
{
    class Program
    {
        private static IConfiguration _config;
        private static string txtFile;

        static void Main(string[] args)
        {
            InitializeConfiguration();
            txtFile = _config.GetValue<string>("TextFile");

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
