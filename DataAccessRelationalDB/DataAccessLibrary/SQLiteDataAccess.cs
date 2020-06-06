using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SQLite;
using Dapper;
using System.Linq;

namespace DataAccessLibrary
{
    class SQLiteDataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connString)
        {
            //create a connection to db with using statement
            //using will always close out of connection properly 
            using (IDbConnection connection = new SQLiteConnection(connString))
            {
                //once connection is created query connection
                //Query<T> where we pass in the model of data
                List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();

                //list of rows in strongly typed models
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connString)
        {
            using (IDbConnection connection = new SQLiteConnection(connString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
