namespace BaseCore.Models
{
    public class Message
    {
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public MessageType MessageType { get; set; }
        public bool ShowMessage { get; set; }
    }
}
