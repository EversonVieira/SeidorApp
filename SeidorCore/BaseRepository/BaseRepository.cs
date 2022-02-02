using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using BaseCore.DataBase.Abstractions;
using BaseCore.DataBase.Factory;
using BaseCore.Extensions;
using BaseCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCore.BaseRepository
{
    public class BaseRepository: Repository
    {
        protected const string BaseModelColumns = $@"CreatedBy, CreatedOn, ModifiedBy, ModifiedOn";
        protected const string BaseModelInsertsParameters = $@"@CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn";
        protected const string BaseModelUpdate = $@"CreatedBy = @CreatedBy, CreatedOn = @CreatedOn, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn";
        public BaseRepository(IDBConnectionFactory connectionFactory, ILogger<BaseRepository> logger):base(connectionFactory,logger)
        {

        }

        public Dictionary<string, dynamic> RetrieveFilterParameters(List<Filter> filters)
        {
            Dictionary<string, dynamic> parameters = new();

            if (filters == null) return parameters;

            foreach (Filter filter in filters)
            {
                if (parameters.ContainsKey($"@{filter.Target1}")) continue;

                parameters.Add($"@{filter.Target1.TargetAsParameter()}", filter.Value1);
                if (filter.Value2 != null)
                    parameters.Add($"@{filter.Target2.TargetAsParameter()}", filter.Value2);

            }
            return parameters;
        }

        public string RetrieveFilterWhereClause(List<Filter> filters)
        {
            StringBuilder whereClause = new();
            if (filters == null || !filters.Any()) return whereClause.ToString();

            whereClause.AppendLine(" WHERE ");
            List<Filter> sortedFilters = filters.OrderBy(x => x.FilterGroup).ToList();
            bool EncapsulateByGroup = sortedFilters.Exists(f => f.FilterGroup > 0);
            int lastGroupId = 0;
            foreach (Filter filter in sortedFilters)
            {
                if (EncapsulateByGroup)
                {
                    if (filter.FilterGroup > lastGroupId)
                    {
                        lastGroupId = filter.FilterGroup;
                        whereClause.Append("(");
                    }
                    bool includeAggregateInsideGroup = sortedFilters.IndexOf(filter) < sortedFilters.FindLastIndex(f => f.FilterGroup == lastGroupId);
                    bool includeAggregateOutsideGroup = sortedFilters.IndexOf(filter) < sortedFilters.Count - 1;
                    whereClause.Append(GetWhereSnippet(filter, includeAggregateInsideGroup));

                    if (!includeAggregateInsideGroup)
                    {
                        whereClause.Append(")");
                        if (includeAggregateOutsideGroup)
                            whereClause.Append($" {filter.GetAggregateType()}");
                    }

                }
                else
                {
                    whereClause.Append(GetWhereSnippet(filter, !filter.Equals(sortedFilters.Last())));
                }
            }

            return whereClause.ToString();
        }



        public string GetWhereSnippet(Filter filter, bool includeAggregate)
        {
            string snippet = string.Empty;

            if (filter.Value2 != null)
            {
                if (filter.OperationType == FilterOperationType.Between)
                {
                    snippet = $"{filter.Target1} {filter.GetOperationType()} @{filter.Target1.TargetAsParameter()} AND @{filter.Target2.TargetAsParameter()} ";
                    return snippet;
                }
            }

            snippet = $"{filter.Target1} {filter.GetOperationType()} @{filter.Target1.TargetAsParameter()} {(includeAggregate ? $"{filter.GetAggregateType()}":String.Empty)} ";
            return snippet;
        }

        public string GetOffSetAndLimit()
        {
            return $"LIMIT @Limit OFFSET @Offset";
        }

        public Dictionary<string, int> GetPaginationParameters(int limit, int pageIndex)
        {
            Dictionary<string, int> parameters = new();
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", (pageIndex - 1) * limit);

            return parameters;
        }

        public void AddBaseModelParameters(Dictionary<string, dynamic> parameters, BaseModel model)
        {
            parameters.Add($@"@{nameof(BaseModel.CreatedBy)}", model.CreatedBy);
            parameters.Add($@"@{nameof(BaseModel.CreatedOn)}", model.CreatedOn);
            parameters.Add($@"@{nameof(BaseModel.ModifiedBy)}", model.ModifiedBy);
            parameters.Add($@"@{nameof(BaseModel.ModifiedOn)}", model.ModifiedOn);
        }

        public void HandleWithException(BaseResponse response, Exception ex, ILogger logger)
        {
            string errorCode = DateTime.Now.ToString("yyyyMMddhm");
            response.AddErrorMessage(errorCode, $"Um erro inesperado ocorreu, entre em contato com o suporte e informe o código: {errorCode}");
            logger.LogError(ex, $"A Exception has happen with error code: {errorCode} and Message: {ex.Message}");
        }

        public BaseModel FillBaseModel(DbDataReader reader, BaseModel model = null)
        {
            BaseModel returnData = model;
            returnData ??= new();

            returnData.CreatedBy = reader["CreatedBy"].ToString();
            returnData.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
            returnData.ModifiedBy = reader["ModifiedBy"].ToString();
            returnData.ModifiedOn = Convert.ToDateTime(reader["ModifiedOn"]);

            return returnData;
        }
    }

}
