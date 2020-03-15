namespace CrossTech.ClientApi.Models.Authorization
{
    /// <summary>
    /// Тело запроса на получение токена
    /// </summary>
    public class GetAccessTokenRequest
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
