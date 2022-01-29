namespace SeidorCore.Models
{
    public class Request<T> : BaseRequest
    {
        public T? model { get; set; }
    }
}
