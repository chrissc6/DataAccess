using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
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
    }
}
