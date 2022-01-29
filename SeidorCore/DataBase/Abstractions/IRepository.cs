using BaseCore.Models;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace BaseCore.DataBase.Abstractions
{
    public interface IRepository
    {
        Response<bool> OpenConnection();
        Response<bool> CloseConnection();
        Response<bool> BeginTransaction();
        Response<bool> EndTransaction();
        DbCommand CreateCommand(string sql, Dictionary<string, dynamic> parameters = null);
        void ExecuteNonQuery(DbCommand command);
        Task ExecuteNonQueryAsync(DbCommand command);
        Response<DbDataReader>ExecuteReader(DbCommand command);
        Task<DbDataReader> ExecuteReaderAsync(DbCommand command);
        int ExecuteScalar(DbCommand command);
        Task<int> ExecuteScalarAsync(DbCommand command);
        dynamic ExecuteScalarDynamic(DbCommand command);
        dynamic ExecuteScalarDynamicAsync(DbCommand command);
    }
}