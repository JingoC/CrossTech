using System.ComponentModel.DataAnnotations.Schema;

namespace CrossTechTask.DAL.Entity
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
    }
}
