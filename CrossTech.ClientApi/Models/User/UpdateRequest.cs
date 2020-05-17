namespace CrossTech.ClientApi.Models.User
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
