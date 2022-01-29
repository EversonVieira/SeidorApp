using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.DataBase
{
    public static class SqliteDataBaseIntegrityService
    {

        private const string CREATE_USER =
$@"CREATE TABLE IF NOT EXISTS User(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Password TEXT, CreatedBy TEXT, CreatedOn TEXT, ModifiedBy TEXT, ModifiedOn TEXT);";

        private const string CREATE_CPF =
$@"CREATE TABLE IF NOT EXISTS Cpf(ID INTEGER PRIMARY KEY AUTOINCREMENT, OwnerName TEXT, IsBlocked INTEGER, CreatedBy TEXT, CreatedOn TEXT, ModifiedBy TEXT, ModifiedOn TEXT);";


        public static void ValidateIntegrityAndBuildDB(string fileName, string connectionString)
        {
            if (!File.Exists(fileName))
                File.Create(fileName);


            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                CreateTables(connection);
                connection.Close();
            }
        }


        private static void CreateTables(SqliteConnection connection)
        {
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = CREATE_USER;
                command.ExecuteNonQuery();

                command.CommandText = CREATE_CPF;
                command.ExecuteNonQuery();
            }
        }
    }
}
