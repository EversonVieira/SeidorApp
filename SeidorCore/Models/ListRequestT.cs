namespace SeidorCore.Models
{
    public class ListRequest<T> : Request<List<T>>
    {
        public int Limit { get; set; }
        public int PageIndex { get; set; }
    }
}
