namespace CrossTech.ClientApi.Models.Authorization
{
    public class LoginRequest : BaseRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
