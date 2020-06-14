using EFCoreUI.DataAccess;
using EFCoreUI.Models;
using System;
using System.Linq;

namespace EFCoreUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateContact();
            ReadAll();
            Console.WriteLine("done ef");
            Console.ReadLine();
        }

        private static void CreateContact()
        {
            var c = new Contact
            {
                FirstName = "fName1",
                LastName = "lName1"
            };
            c.EmailAddresses.Add(new Email { EmailAddress = "emailA1@mail.com" });
            c.EmailAddresses.Add(new Email { EmailAddress = "emailA2@mail.com" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "513-555-0001" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "513-555-0002" });

            using (var db = new ContactContext())
            {
                db.Contacts.Add(c);
                db.SaveChanges();
            }
        }

        private static void ReadAll()
        {
            using (var db = new ContactContext())
            {
                var records = db.Contacts.ToList();

                foreach (var i in records)
                {
                    Console.WriteLine($"{i.FirstName}, {i.LastName}");
                }
            }
        }
    }
}
