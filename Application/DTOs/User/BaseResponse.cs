namespace Application.DTOs.User
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public BaseResponse(string message)
        {
            Message = message;
        }
    }
}
