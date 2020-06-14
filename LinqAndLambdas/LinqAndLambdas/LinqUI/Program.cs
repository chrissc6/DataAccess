using System;

namespace LinqUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("done linq");
            Console.ReadLine();
        }

        private static void LambdaTest()
        {
            var data = SampleData.GetContactData();
        }
    }
}
