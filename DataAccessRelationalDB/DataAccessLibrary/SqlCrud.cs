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
    }
}
