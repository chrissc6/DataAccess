using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        public List<ContactModel> ReadAllRecords(string txtFl)
        {
            if (File.Exists(txtFl) == false)
            {
                return new List<ContactModel>();
            }

            var lines = File.ReadAllLines(txtFl);
            List<ContactModel> output = new List<ContactModel>();

            foreach (var l in lines)
            {
                var vals = l.Split(',');
                ContactModel c = new ContactModel();

                if (vals.Length < 4)
                {
                    throw new Exception($"Invlaid row: {l}");
                }

                c.FirstName = vals[0];
                c.LastName = vals[1];
                c.PhoneNumbers = vals[2].Split(';').ToList();
                c.EmailAddresses = vals[3].Split(';').ToList();

                output.Add(c);
            }

            return output;
        }

        public void WriteAllRecords(List<ContactModel> contacts, string txtFl)
        {
            List<string> lines = new List<string>();

            foreach (var c in contacts)
            {
                lines.Add($"{c.FirstName}, {c.LastName}, {String.Join(';', c.PhoneNumbers)}, {String.Join(';', c.EmailAddresses)}");
            }

            File.WriteAllLines(txtFl, lines);
        }
    }
}
