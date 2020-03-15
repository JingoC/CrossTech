namespace CrossTech.ClientApi.Models.Authorization
{
    /// <summary>
    /// Тело запрос на операцию входа
    /// </summary>
    public class LoginRequest : BaseRequest
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
