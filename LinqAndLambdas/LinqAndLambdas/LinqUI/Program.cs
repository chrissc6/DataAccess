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

            var results = data.Where(x => x.Addresses.Count > 1);

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
