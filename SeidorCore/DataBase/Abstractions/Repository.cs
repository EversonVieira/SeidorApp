using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SeidorCore.DataBase.Factory;
using SeidorCore.Models;

namespace SeidorCore.DataBase.Abstractions
{
    public class Repository : IRepository
    {
        private IDBConnectionFactory _dBConnectionFactory;
        private DbConnection dbConnection;
        private DbTransaction dbTransaction;
        private ILogger _logger;

        public Repository(IDBConnectionFactory dBConnectionFactory, ILogger<Repository> logger)
        {
            this._logger = logger;
            this._dBConnectionFactory = dBConnectionFactory;
            GetConnectionFromFactory();
        }

        private void GetConnectionFromFactory()
        {
            if (this.dbConnection != null && this.dbConnection.State is >= ConnectionState.Open and < ConnectionState.Broken) return;
            
            this.dbConnection = _dBConnectionFactory.GetConnection();
            this.dbConnection.Open();
        }
        public DbCommand CreateCommand(string sql, Dictionary<string, dynamic> parameters = null)
        {
            GetConnectionFromFactory();
            DbCommand command = dbConnection.CreateCommand();
            command.CommandText = sql;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, dynamic> parameter in parameters)
                {
                    DbParameter param = command.CreateParameter();
                    param.ParameterName = parameter.Key;
                    param.Value = parameter.Value;
                    command.Parameters.Add(param);
                }

            }
            return command;
        }

        public int ExecuteScalar(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public T ExecuteScalar<T>(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                return (T) Convert.ChangeType(command.ExecuteScalar(), typeof(T));
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public dynamic ExecuteScalarDynamic(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                return command.ExecuteScalar();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void ExecuteNonQuery(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public Response<DbDataReader> ExecuteReader(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                return new()
                {
                   Data = command.ExecuteReader()
                };
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<int> ExecuteScalarAsync(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        public dynamic ExecuteScalarDynamicAsync(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                return command.ExecuteScalarAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task ExecuteNonQueryAsync(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<DbDataReader> ExecuteReaderAsync(DbCommand command)
        {
            GetConnectionFromFactory();
            _logger.LogInformation($"Attempting to run the following sql: {command.CommandText}");
            try
            {
                return await command.ExecuteReaderAsync();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Response<bool> BeginTransaction()
        {
            Response<bool> response = new();
            GetConnectionFromFactory();

            try
            {
                this.dbTransaction ??= this.dbConnection.BeginTransaction();
                response.Data = true;
            }
            catch (DbException ex)
            {
                response.AddExceptionMessage("1000", ex.Message);
                response.Data = false;
            }

            return response;
        }

        public Response<bool> EndTransaction()
        {
            Response<bool> response = new();
            GetConnectionFromFactory();

            try
            {
                this.dbTransaction.Commit();
                response.Data = true;
            }
            catch (DbException ex)
            {
                response.Merge(this.RollbackTransaction());
                response.AddExceptionMessage("1000", ex.Message);
            }
            finally
            {
                this.dbConnection.Dispose();
                this.dbConnection = null;
            }

            return response;
        }
        private Response<bool> RollbackTransaction()
        {
            Response<bool> response = new();
            GetConnectionFromFactory();

            try
            {
                this.dbTransaction.Rollback();
                response.Data = true;
            }
            catch (DbException ex)
            {
                response.AddCautionMessage("1000", ex.Message);
            }

            return response;
        }

        public Response<bool> OpenConnection()
        {
            Response<bool> response = new();
            try
            {
                if (this.dbConnection.State == System.Data.ConnectionState.Closed)
                {
                    this.dbConnection.Open();
                    response.Data = true;
                }
            }
            catch (DbException ex)
            {
                response.AddCautionMessage("1000", ex.Message);
            }

            return response;
        }

        public Response<bool> CloseConnection()
        {
            Response<bool> response = new();
            try
            {
                if (this.dbConnection == null || this.dbConnection.State != System.Data.ConnectionState.Open)
                {
                    _logger.LogWarning("Connection already closed");
                    return response;
                }
                if (this.dbTransaction != null)
                {
                    response.Merge(EndTransaction());
                }

                if (this.dbConnection != null && this.dbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.dbConnection.Dispose();
                    this.dbConnection = null;
                    response.Data = true;
                }
            }
            catch (DbException ex)
            {
                response.AddCautionMessage("1000", ex.Message);
            }

            return response;
        }
    }
}
