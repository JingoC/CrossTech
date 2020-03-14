using System;
using System.Collections.Generic;
using System.Text;

namespace CrossTech.ClientApi.Models.Authorization
{
    public class UserModel
    {
        public string Login { get; set; }
        public string AccessToken { get; set; }
        public List<ClaimModel> Claims { get; set; }
    }
}
