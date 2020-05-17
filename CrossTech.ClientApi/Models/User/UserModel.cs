using System.Collections.Generic;

namespace CrossTech.ClientApi.Models.User
{
    public class UserModel
    {
        public string Login { get; set; }
        public string AccessToken { get; set; }
        public List<ClaimModel> Claims { get; set; }
    }
}
