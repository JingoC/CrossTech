namespace CrossTech.ClientApi.Models
{
    /// <summary>
    /// Базовый класс для всех запросов к Api
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// Token доступа
        /// </summary>
        public string AccessToken { get; set; }
    }
}
