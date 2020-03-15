namespace CrossTech.ClientApi.Models.Authorization
{
    /// <summary>
    /// Тело ответа на запрос получения токена доступа
    /// </summary>
    public class GetAccessTokenResponse : BaseResponse
    {
        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }
    }
}
