using EFCoreUI.DataAccess;
using EFCoreUI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCoreUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateContact();
            //ReadAll();
            //CreateContact2();
            //UpdateFirstName(2, "lName2z");
            //ReadById(2);
            //RemovePhoneNumber(2, "513-555-0001");
            //RemoveContact(2);
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

        private static void CreateContact2()
        {
            var c = new Contact
            {
                FirstName = "fName2",
                LastName = "lName2"
            };
            c.EmailAddresses.Add(new Email { EmailAddress = "emailB1@mail.com" });
            c.EmailAddresses.Add(new Email { EmailAddress = "emailB2@mail.com" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "513-555-0001" });
            c.PhoneNumbers.Add(new Phone { PhoneNumber = "513-555-1002" });

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
                var records = db.Contacts
                    .Include(e => e.EmailAddresses)
                    .Include(p => p.PhoneNumbers)
                    .ToList();

                /// <summary>
                /// pulled from sql profiler
                /// SELECT [c].[Id], [c].[FirstName], [c].[LastName], [e].[Id], [e].[ContactId], [e].[EmailAddress], [p].[Id], [p].[ContactId], [p].[PhoneNumber]
                /// FROM[Contacts] AS[c]
                /// LEFT JOIN[EmailAddresses] AS[e] ON[c].[Id] = [e].[ContactId]
                /// LEFT JOIN[PhoneNumbers] AS[p] ON[c].[Id] = [p].[ContactId]
                /// ORDER BY[c].[Id], [e].[Id], [p].[Id]
                /// </summary>

                foreach (var i in records)
                {
                    Console.WriteLine($"{i.FirstName}, {i.LastName}");
                }
            }
        }

        private static void ReadById(int id)
        {
            using (var db = new ContactContext())
            {
                var i = db.Contacts.Where(x => x.Id == id).First();
                Console.WriteLine($"{i.FirstName}, {i.LastName}");
            }
        }

        private static void UpdateFirstName(int id, string fname)
        {
            using (var db = new ContactContext())
            {
                var i = db.Contacts.Where(x => x.Id == id).First();
                i.FirstName = fname;
                db.SaveChanges();
            }
        }

        private static void RemovePhoneNumber(int id, string pNum)
        {
            using (var db = new ContactContext())
            {
                var i = db.Contacts
                    .Include(p => p.PhoneNumbers)
                    .Where(x => x.Id == id).First();

                i.PhoneNumbers.RemoveAll(p => p.PhoneNumber == pNum);

                db.SaveChanges();
            }
        }

        private static void RemoveContact(int id)
        {
            using (var db = new ContactContext())
            {
                var i = db.Contacts
                    .Include(e => e.EmailAddresses)
                    .Include(p => p.PhoneNumbers)
                    .Where(x => x.Id == id).First();

                db.Contacts.Remove(i);

                db.SaveChanges();
            }
        }
    }
}
