using Dapper;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly string connString;
        private SqlDataAccess db = new SqlDataAccess();

        public SqlCrud(string connString)
        {
            this.connString = connString;
        }

        public List<BasicContactModel> GetAllContacts()
        {
            string sql = "SELECT Id, FirstName, LastName FROM dbo.Contacts";

            return db.LoadData<BasicContactModel, dynamic>(sql, new { }, connString);
        }

        public FullContactModel GetFullContactById(int id)
        {
            string sql = "SELECT Id, FirstName, LastName FROM dbo.Contacts WHERE Id = @Id";

            FullContactModel output = new FullContactModel();

            output.BasicInfo = db.LoadData<BasicContactModel, dynamic>(sql, new { Id = id }, connString).FirstOrDefault();

            if (output.BasicInfo == null)
            {
                return null;
            }

            sql = @"SELECT * FROM dbo.EmailAddresses e
                    INNER JOIN dbo.ContactEmail ce ON ce.EmailAddressId = e.Id
                    WHERE ce.ContactId = @Id";

            output.EmailAddresses = db.LoadData<EmailAddressModel, dynamic>(sql, new { Id = id }, connString);

            sql = @"SELECT * FROM dbo.PhoneNumbers p
                    INNER JOIN dbo.ContactPhoneNumbers ce ON ce.PhoneNumberId = p.Id
                    WHERE ce.ContactId = @Id";

            output.PhoneNumbers = db.LoadData<PhoneNumberModel, dynamic>(sql, new { Id = id }, connString);

            return output;
        }

        public void CreateContact(FullContactModel contact)
        {
            //save basic contact
            string sql = "INSERT INTO dbo.Contacts (FirstName, LastName) VALUES (@FirstName, @LastName);";
            db.SaveData(sql, new { FirstName = contact.BasicInfo.FirstName, LastName = contact.BasicInfo.LastName }, connString);

            //get id of the contact
            sql = "SELECT Id FROM dbo.Contacts WHERE FirstName = @FirstName AND LastName = @LastName;";
            int contactId = db.LoadData<IdLookupModel, dynamic>(sql, new { FirstName = contact.BasicInfo.FirstName, LastName = contact.BasicInfo.LastName }, connString).First().Id;

            foreach (var i in contact.PhoneNumbers)
            {
                if (i.Id == 0)
                {
                    sql = "INSERT INTO dbo.PhoneNumbers (PhoneNumber) VALUES (@PhoneNumber);";
                    db.SaveData(sql, new { i.PhoneNumber }, connString);

                    sql = "SELECT ID FROM dbo.PhoneNumbers WHERE PhoneNumber = @PhoneNumber;";
                    i.Id = db.LoadData<IdLookupModel, dynamic>(sql, new { i.PhoneNumber }, connString).First().Id;
                }

                sql = "INSERT INTO dbo.ContactPhoneNumbers (ContactId, PhoneNumberId) VALUES (@ContactId, @PhoneNumberId)";
                db.SaveData(sql, new { ContactId = contactId, PhoneNumberId = i.Id }, connString);
            }

            foreach (var i in contact.EmailAddresses)
            {
                if (i.Id == 0)
                {
                    sql = "INSERT INTO dbo.EmailAddresses (EmailAddress) VALUES (@EmailAddress);";
                    db.SaveData(sql, new { i.EmailAddress }, connString);

                    sql = "SELECT ID FROM dbo.EmailAddresses WHERE EmailAddress = @EmailAddress;";
                    i.Id = db.LoadData<IdLookupModel, dynamic>(sql, new { i.EmailAddress }, connString).First().Id;
                }

                sql = "INSERT INTO dbo.ContactEmail (ContactId, EmailAddressId) VALUES (@ContactId, @EmailAddressId)";
                db.SaveData(sql, new { ContactId = contactId, EmailAddressId = i.Id }, connString);
            }
        }

        public void UpdateContactName(BasicContactModel contact)
        {
            string sql = "UPDATE dbo.Contacts SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id;";
            db.SaveData(sql, contact, connString);
        }
    }
}
