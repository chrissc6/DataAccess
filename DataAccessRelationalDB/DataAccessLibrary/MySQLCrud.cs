using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class MySQLCrud
    {
        private readonly string connString;
        private MySQLDataAccess db = new MySQLDataAccess();

        public MySQLCrud(string connString)
        {
            this.connString = connString;
        }

        public List<BasicContactModel> GetAllContacts()
        {
            string sql = "SELECT Id, FirstName, LastName FROM Contacts";

            return db.LoadData<BasicContactModel, dynamic>(sql, new { }, connString);
        }

        public FullContactModel GetFullContactById(int id)
        {
            string sql = "SELECT Id, FirstName, LastName FROM Contacts WHERE Id = @Id";

            FullContactModel output = new FullContactModel();

            output.BasicInfo = db.LoadData<BasicContactModel, dynamic>(sql, new { Id = id }, connString).FirstOrDefault();

            if (output.BasicInfo == null)
            {
                return null;
            }

            sql = @"SELECT * FROM EmailAddresses e
                    INNER JOIN ContactEmail ce ON ce.EmailAddressId = e.Id
                    WHERE ce.ContactId = @Id";

            output.EmailAddresses = db.LoadData<EmailAddressModel, dynamic>(sql, new { Id = id }, connString);

            sql = @"SELECT * FROM PhoneNumbers p
                    INNER JOIN ContactPhoneNumbers ce ON ce.PhoneNumberId = p.Id
                    WHERE ce.ContactId = @Id";

            output.PhoneNumbers = db.LoadData<PhoneNumberModel, dynamic>(sql, new { Id = id }, connString);

            return output;
        }

        public void CreateContact(FullContactModel contact)
        {
            //save basic contact
            string sql = "INSERT INTO Contacts (FirstName, LastName) VALUES (@FirstName, @LastName);";
            db.SaveData(sql, new { FirstName = contact.BasicInfo.FirstName, LastName = contact.BasicInfo.LastName }, connString);

            //get id of the contact
            sql = "SELECT Id FROM Contacts WHERE FirstName = @FirstName AND LastName = @LastName;";
            int contactId = db.LoadData<IdLookupModel, dynamic>(sql, new { FirstName = contact.BasicInfo.FirstName, LastName = contact.BasicInfo.LastName }, connString).First().Id;

            foreach (var i in contact.PhoneNumbers)
            {
                if (i.Id == 0)
                {
                    sql = "INSERT INTO PhoneNumbers (PhoneNumber) VALUES (@PhoneNumber);";
                    db.SaveData(sql, new { i.PhoneNumber }, connString);

                    sql = "SELECT ID FROM PhoneNumbers WHERE PhoneNumber = @PhoneNumber;";
                    i.Id = db.LoadData<IdLookupModel, dynamic>(sql, new { i.PhoneNumber }, connString).First().Id;
                }

                sql = "INSERT INTO ContactPhoneNumbers (ContactId, PhoneNumberId) VALUES (@ContactId, @PhoneNumberId)";
                db.SaveData(sql, new { ContactId = contactId, PhoneNumberId = i.Id }, connString);
            }

            foreach (var i in contact.EmailAddresses)
            {
                if (i.Id == 0)
                {
                    sql = "INSERT INTO EmailAddresses (EmailAddress) VALUES (@EmailAddress);";
                    db.SaveData(sql, new { i.EmailAddress }, connString);

                    sql = "SELECT ID FROM EmailAddresses WHERE EmailAddress = @EmailAddress;";
                    i.Id = db.LoadData<IdLookupModel, dynamic>(sql, new { i.EmailAddress }, connString).First().Id;
                }

                sql = "INSERT INTO ContactEmail (ContactId, EmailAddressId) VALUES (@ContactId, @EmailAddressId)";
                db.SaveData(sql, new { ContactId = contactId, EmailAddressId = i.Id }, connString);
            }
        }

        public void UpdateContactName(BasicContactModel contact)
        {
            string sql = "UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id;";
            db.SaveData(sql, contact, connString);
        }

        public void RemovePhoneNumber(int contactId, int phoneNumId)
        {
            string sql = "SELECT Id, ContactId, PhoneNumberId FROM ContactPhoneNumbers WHERE PhoneNumberId = @PhoneNumberId;";
            var links = db.LoadData<ContactPhoneNumberModel, dynamic>(sql, new { PhoneNumberId = phoneNumId }, connString);

            sql = "DELETE FROM ContactPhoneNumbers WHERE ContactId = @ContactId AND PhoneNumberId = @PhoneNumberId";
            db.SaveData(sql, new { PhoneNumberID = phoneNumId, ContactId = contactId }, connString);

            if (links.Count == 1)
            {
                sql = "DELETE FROM PhoneNumbers WHERE Id = @PhoneNumberId";
                db.SaveData(sql, new { PhoneNumberID = phoneNumId }, connString);
            }
        }
    }
}
