namespace BaseCore.Models
{
    public enum FilterOperationType
    {
        Equals = 0,
        GreaterThan = 1,
        LessThan = 2,
        GreaterOrEqualThan = 3,
        LessOrEqualThan = 4,
        In = 5,
        
        NotEquals = 6,
        NotGreaterThan = 7,
        NotLessThan = 8,
        NotGreaterOrEqualThan = 9,
        NotLessOrEqualThan = 10,
        NotIn = 11,

        Between = 12,
        Like = 13,
    }
}
