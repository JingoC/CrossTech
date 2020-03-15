namespace CrossTech.ClientApi.Models.Authorization
{
    /// <summary>
    /// Тело ответа на операцию входа
    /// </summary>
    public class LoginResponse : BaseResponse
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserModel User { get; set; }
    }
}
