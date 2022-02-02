using BaseCore.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.DataBase
{
    /// <summary>
    /// Classe responsável por garantir que o banco existe e foi configurado corretamente.
    /// </summary>
    public static class SqliteDataBaseIntegrityService
    {

        private const string CREATE_USER =
$@"CREATE TABLE IF NOT EXISTS User(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Email TEXT, Password TEXT, CreatedBy TEXT, CreatedOn TEXT, ModifiedBy TEXT, ModifiedOn TEXT);";

        private const string CREATE_CPF =
$@"CREATE TABLE IF NOT EXISTS Cpf(ID INTEGER PRIMARY KEY AUTOINCREMENT, OwnerName TEXT, Document TEXT, IsBlocked INTEGER, CreatedBy TEXT, CreatedOn TEXT, ModifiedBy TEXT, ModifiedOn TEXT);";

        private const string CREATE_SESSION =
$@"CREATE TABLE IF NOT EXISTS Session(ID INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER, KEY TEXT, LastUse TEXT);";


        public static void ValidateIntegrityAndBuildDB(string fileName, string connectionString)
        {
            if (fileName.IsNotNullOrEmpty())
            {
                FileInfo file = new FileInfo(fileName);
                bool justWait = false;
                while (!file.Exists)
                {
                    if(!justWait)
                        file.Create();
                
                    justWait = true;
                }
            }

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

                command.CommandText = CREATE_SESSION;
                command.ExecuteNonQuery();
            }
        }
    }
}
