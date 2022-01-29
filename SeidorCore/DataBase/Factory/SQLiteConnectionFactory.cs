using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.DataBase.Factory
{
    public class SQLiteConnectionFactory:IDBConnectionFactory
    {
        private SqliteConnection Connection;
        private string _connectionString;
        public SQLiteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection GetConnection()
        {
            if (this.Connection != null && this.Connection.State is >= ConnectionState.Open and < ConnectionState.Broken) return this.Connection;

            this.Connection = new SqliteConnection(_connectionString);
            return this.Connection;
        }
    }
}
