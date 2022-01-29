namespace BaseCore.Models
{
    public class ListRequest:BaseRequest
    {
        public int Limit { get; set; }
        public int PageIndex { get; set; }
        public List<Filter> Filters { get; set; } = new();

    }
}
