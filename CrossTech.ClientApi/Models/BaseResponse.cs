namespace CrossTech.ClientApi.Models
{
    /// <summary>
    /// Базовый класс на все ответы от Api
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Результат операции
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; } = string.Empty;

        static public BaseResponse GetFail(string message) => new BaseResponse() { IsSuccess = false, Message = message };
    }
}
