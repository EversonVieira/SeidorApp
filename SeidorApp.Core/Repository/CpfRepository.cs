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
    internal class CpfRepository:BaseRepository
    {
        private readonly ILogger _logger;

        private const string INSERT =
$@"INSERT INTO Cpf(OwnerName,Document,IsBlocked,{BaseModelColumns}) VALUES(,@OwnerName,@Document,@IsBlocked,{BaseModelInsertsParameters}); SELECT last_insert_rowid()";

        private const string UPDATE =
$@"UPDATE Cpf SET OwnerName = @OwnerName, Document = @Document, {BaseModelUpdate} Where Id = @Id";

        private const string DELETE =
$@"DELETE FROM Cpf Where Id = @Id ";

        private const string SELECT =
$@"SELECT Id,Name,Password,{BaseModelColumns} From Cpf ";


        public CpfRepository(IDBConnectionFactory connectionFactory, ILogger<CpfRepository> logger) : base(connectionFactory, logger)
        {
            _logger = logger;
        }

        public Response<long> Insert(Cpf cpf)
        {
            Response<long> response = new Response<long>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(Cpf.OwnerName)}", cpf.OwnerName);
                parameters.Add($"@{nameof(Cpf.Document)}", cpf.Document);
                parameters.Add($"@{nameof(Cpf.IsBlocked)}", cpf.IsBlocked);
                base.AddBaseModelParameters(parameters, cpf);

                using (DbCommand cmd = base.CreateCommand(INSERT, parameters))
                {
                    response.Data = ExecuteScalar<long>(cmd);
                    /* Everything in the code is in english, but the messages could be in portuguese, since the application
                       is based on Brazil stuff.
                     */
                    response.AddSuccessMessage("001", "Usuário inserido com sucesso!");
                    response.StatusCode = HttpStatusCode.Created;
                }
            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex, _logger);
            }

            return response;
        }

        public Response<bool> Update(Cpf cpf)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(Cpf.Id)}", cpf.Id);
                parameters.Add($"@{nameof(Cpf.OwnerName)}", cpf.OwnerName);
                parameters.Add($"@{nameof(Cpf.Document)}", cpf.Document);
                base.AddBaseModelParameters(parameters, cpf);

                using (DbCommand cmd = base.CreateCommand(UPDATE, parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.Data = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.AddSuccessMessage("001", "CPF atualizado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex, _logger);
            }

            return response;
        }

        public Response<bool> Delete(long cpfId)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
                parameters.Add($"@{nameof(Cpf.Id)}", cpfId);

                using (DbCommand cmd = base.CreateCommand(DELETE, parameters))
                {
                    ExecuteNonQuery(cmd);
                    response.Data = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.AddSuccessMessage("001", "CPF removido com sucesso!");
                }
            }
            catch (Exception ex)
            {
                base.HandleWithException(response, ex, _logger);
            }

            return response;
        }

        public ListResponse<Cpf> FindByRequest(Request request)
        {
            ListResponse<Cpf> response = new ListResponse<Cpf>();

            try
            {
                Dictionary<string, dynamic> parameters = base.RetrieveFilterParameters(request.filters);
                string query = $"{SELECT}{RetrieveFilterWhereClause(request.filters)}";
                using (DbCommand cmd = base.CreateCommand(SELECT, parameters))
                {
                    using (var reader = base.ExecuteReader(cmd).Data)
                    {
                        response.Data = new List<Cpf>();

                        while (reader.Read())
                        {
                            response.Data.Add(FillCpf(reader));
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

        private Cpf FillCpf(DbDataReader reader)
        {
            Cpf cpf = new Cpf();

            cpf.Id = reader["Id"].IsNotNull() ? Convert.ToInt64(reader["Id"]) : 0;
            cpf.OwnerName = reader["OwnerName"].IsNotNull() ? reader["OwnerName"].ToString() : string.Empty;
            cpf.Document = reader["Document"].IsNotNull() ? reader["Document"].ToString() : string.Empty;

            base.FillBaseModel(reader, cpf);

            return cpf;
        }
    }
}
