namespace Application.DTOs
{
    public class ResponseBase
    {
        public string Message { get; set; }

        public ResponseBase(string message)
        {
            Message = message;
        }
    }
}
