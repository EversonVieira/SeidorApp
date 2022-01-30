using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using BaseCore.BaseRepository;
using BaseCore.DataBase.Factory;
using BaseCore.Extensions;
using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Repository
{
    internal class SessionRepository:BaseRepository
    {
        private readonly ILogger _logger;

        private const string INSERT =
$@"INSERT INTO Session(UserId, Key, LastUse) VALUES(@UserId, @Key, @LastUse); SELECT last_insert_rowid()";

        private const string SELECT =
$@"SELECT Id, UserId, Key, LastUse FROM Session";

        private const string DELETE =
$@"DELETE FROM Session WHERE Key = @Key";


        public SessionRepository(IDBConnectionFactory connectionFactory, ILogger<SessionRepository> logger) : base(connectionFactory, logger)
        {
            _logger = logger;
        }

        public Response<long> Insert(Session session)
        {
            Response<long> response = new Response<long>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(Session.UserId)}", session.UserId);
                parameters.Add($"@{nameof(Session.Key)}", session.Key);
                parameters.Add($"@{nameof(Session.LastUse)}", session.LastUse);

                using (DbCommand cmd = base.CreateCommand(INSERT, parameters))
                {
                    response.Data = ExecuteScalar<long>(cmd);
                    response.StatusCode = HttpStatusCode.Created;
                }
            }
            catch(Exception ex)
            {
                HandleWithException(response, ex, _logger);
            }


            return response;
        }

        public ListResponse<Session> FindByRequest(Request request)
        {

            ListResponse<Session> response = new ListResponse<Session>();

            try
            {
                Dictionary<string, dynamic> parameters = base.RetrieveFilterParameters(request.filters);
                string query = $"{SELECT}{RetrieveFilterWhereClause(request.filters)}";
                using (DbCommand cmd = base.CreateCommand(query, parameters))
                {
                    using (DbDataReader reader = base.ExecuteReader(cmd).Data)
                    {
                        response.Data = new List<Session>();

                        while (reader.Read())
                        {
                            response.Data.Add(FillSession(reader));
                        }
                    }

                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex, _logger);
            }

            return response;
        }

        public Response<bool> Delete(string key)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(Session.Key)}", key);

                using (DbCommand cmd = base.CreateCommand(DELETE, parameters))
                {
                    response.Data = ExecuteScalar<bool>(cmd);
                    response.StatusCode = HttpStatusCode.Created;
                }
            }
            catch (Exception ex)
            {
                HandleWithException(response, ex, _logger);
            }


            return response;
        }

        public Response<bool> UpdateLastUse(string key)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(Session.LastUse)}", DateTime.UtcNow);
                parameters.Add($"@{nameof(Session.Key)}", key);

                using (DbCommand cmd = base.CreateCommand("UPDATE Session SET LastUse = @LastUse WHERE Key = @Key", parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.Data = true;
                    response.StatusCode = HttpStatusCode.Created;
                }
            }
            catch (Exception ex)
            {
                HandleWithException(response, ex, _logger);
            }


            return response;
        }

        private Session FillSession(DbDataReader reader)
        {
            Session session = new Session();

            session.Id = reader["Id"].IsNotNull() ? Convert.ToInt64(reader["Id"]) : 0;
            session.UserId = reader["UserId"].IsNotNull() ? Convert.ToInt64(reader["UserId"]) : 0;
            session.Key = reader["Key"].IsNotNull() ? reader["Key"].ToString() : string.Empty;
            session.LastUse = reader["LastUse"].IsNotNull() ? Convert.ToDateTime(reader["LastUse"]) : DateTime.MinValue;

            return session;
        }
    }
}
