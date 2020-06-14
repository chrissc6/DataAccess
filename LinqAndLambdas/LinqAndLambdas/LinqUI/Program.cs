using LinqUI.Models;
using System;
using System.Linq;

namespace LinqUI
{
    class Program
    {
        static void Main(string[] args)
        {
            LambdaTest();
            Console.WriteLine("done linq");
            Console.ReadLine();
        }

        private static void LambdaTest()
        {
            var data = SampleData.GetContactData();

            //WHERE
            //var results = data.Where(x => x.Addresses.Count > 1);
            //foreach (var i in results)
            //{
            //    Console.WriteLine($"{i.FirstName}, {i.LastName}");
            //}

            //SELECT
            //var results = data.Select(x => x.FirstName);
            //foreach (var i in results)
            //{
            //    Console.WriteLine(i);
            //}

            //TAKE
            //var results = data.Take(2);
            //foreach (var i in results)
            //{
            //    Console.WriteLine($"{i.FirstName}, {i.LastName}");
            //}

            //SKIP & TAKE
            //var results = data.Skip(2).Take(2);
            //foreach (var i in results)
            //{
            //    Console.WriteLine($"{i.FirstName}, {i.LastName}");
            //}

            //ORDERBY
            //var results = data.OrderBy(x => x.LastName);
            //foreach (var i in results)
            //{
            //    Console.WriteLine($"{i.FirstName}, {i.LastName}");
            //}

            //OrderByDescending
            var results = data.OrderByDescending(x => x.LastName);
            foreach (var i in results)
            {
                Console.WriteLine($"{i.FirstName}, {i.LastName}");
            }
        }

        //data.Where(x => x.Addresses.Count > 1);
        private static bool WhereMethod(ContactModel x)
        {
            //if (x.Addresses.Count > 1)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return x.Addresses.Count > 1;
        }
    }
}
