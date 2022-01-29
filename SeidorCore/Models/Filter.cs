namespace BaseCore.Models
{
    public class Filter
    {
        public int FilterGroup { get; set; }
        public FilterOperationType OperationType { get; set; }
        public FilterAggregateType AggregateType { get; set; }
        public string? Target1 { get; set; }
        public string? Target2 { get; set; }
        public dynamic? Value1 { get; set; }
        public dynamic? Value2 { get; set; }

        public string GetOperationType()
        {
            return this.OperationType switch
            {
                FilterOperationType.Equals => "=",
                FilterOperationType.GreaterThan => ">",
                FilterOperationType.LessThan => "<",
                FilterOperationType.GreaterOrEqualThan => ">=",
                FilterOperationType.LessOrEqualThan => "<=",
                FilterOperationType.In => "IN",

                FilterOperationType.NotEquals => "<>",
                FilterOperationType.NotGreaterThan => "NOT >",
                FilterOperationType.NotLessThan => "NOT <",
                FilterOperationType.NotGreaterOrEqualThan => "NOT >=",
                FilterOperationType.NotLessOrEqualThan => "NOT <=",
                FilterOperationType.NotIn => "NOT IN",

                FilterOperationType.Between => "BETWEEN",
                FilterOperationType.Like => "LIKE",
                _ => "",
            };
        }

        public string GetAggregateType()
        {
            return this.AggregateType switch
            {
                FilterAggregateType.AND => "AND",
                FilterAggregateType.OR => "OR",
                _ => string.Empty
            };
        }
    }
}
