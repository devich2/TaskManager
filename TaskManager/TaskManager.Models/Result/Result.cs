namespace TaskManager.Models.Result
{
    public class Result
    {
        public ResponseMessageType Message { get; set; }
        public string MessageDetails { get; set; }
        public ResponseStatusType ResponseStatusType { get; set; }
    }
}
