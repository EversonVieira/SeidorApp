using Microsoft.Extensions.Logging;
using SeidorApp.Core.Models;
using SeidorCore.BaseRepository;
using SeidorCore.DataBase.Factory;
using SeidorCore.Extensions;
using SeidorCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Repository
{
    internal class UserRepository:BaseRepository
    {
        private readonly ILogger _logger;

        private const string INSERT =
$@"INSERT INTO User(Id,Name,Password,{BaseModelColumns}) VALUES(@Id,@Name,@Password,{BaseModelInsertsParameters}) SELECT last_insert_rowid()";

        private const string UPDATE =
$@"UPDATE User SET Id = @Id, Name = @Name, Password = @Password, {BaseModelUpdate}";

        private const string DELETE =
$@"DELETE FROM User Where Id = @Id ";

        private const string SELECT =
$@"SELECT Id,Name,Password,{BaseModelColumns} From User ";
        public UserRepository(IDBConnectionFactory connectionFactory, ILogger<UserRepository> logger):base(connectionFactory, logger)
        {
            _logger = logger;
        }

        public Response<long> Insert(User user)
        {
            Response<long> response = new Response<long>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(User.Email)}", user.Email);
                parameters.Add($"@{nameof(User.Password)}", user.Password);
                base.AddBaseModelParameters(parameters, user);

                using (DbCommand cmd =  base.CreateCommand(INSERT, parameters))
                {
                    response.Data = ExecuteScalar<long>(cmd);
                    /* Everything in the code is in english, but the messages could be in portuguese, since the application
                       is based on Brazil stuff.
                     */
                    response.AddSuccessMessage("001", "Usuário inserido com sucesso!");
                    response.StatusCode = HttpStatusCode.Created;
                }
            }
            catch(Exception ex)
            {
                base.HandleWithException(response, ex, _logger);
            }

            return response;
        }

        public Response<bool> Update(User user)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(User.Id)}", user.Id);
                parameters.Add($"@{nameof(User.Email)}", user.Email);
                parameters.Add($"@{nameof(User.Password)}", user.Password);
                base.AddBaseModelParameters(parameters, user);

                using (DbCommand cmd = base.CreateCommand(UPDATE, parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.Data = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.AddSuccessMessage("001", "Usuário atualizado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex, _logger);
            }

            return response;
        }

        public Response<bool> Delete(long userId)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(User.Id)}", userId);

                using (DbCommand cmd = base.CreateCommand(DELETE, parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.Data = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.AddSuccessMessage("001", "Usuário removido com sucesso!");
                }
            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex, _logger);
            }

            return response;
        }

        public ListResponse<User> FindByRequest(Request request)
        {
            ListResponse<User> response = new ListResponse<User>();

            try
            {
                Dictionary<string, dynamic> parameters = base.RetrieveFilterParameters(request.filters);
                string query = $"{SELECT}{RetrieveFilterWhereClause(request.filters)}";
                using (DbCommand cmd = base.CreateCommand(SELECT, parameters))
                {
                    using (var reader = base.ExecuteReader(cmd).Data)
                    {
                        response.Data = new List<User>();

                        while (reader.Read())
                        {
                            response.Data.Add(FillUser(reader));
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

        private User FillUser(DbDataReader reader)
        {
            User user = new User();

            user.Id = reader["id"].IsNotNull() ? Convert.ToInt64(reader["Id"]):0;
            user.Name = reader["Name"].IsNotNull() ? reader["Name"].ToString():string.Empty;
            user.Email = reader["Email"].IsNotNull() ? reader["Email"].ToString():string.Empty;

            base.FillBaseModel(reader,user);

            return user;
        }
    }
}
