using System;
using System.Collections.Generic;
using System.Text;

namespace CrossTech.ClientApi.Models.Authorization
{
    /// <summary>
    /// Модель пользователь
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Список доступных прав
        /// </summary>
        public List<ClaimModel> Claims { get; set; }
    }
}
