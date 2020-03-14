namespace CrossTech.ClientApi.Models
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        static public BaseResponse GetFail(string message) => new BaseResponse() { IsSuccess = false, Message = message };
    }
}
