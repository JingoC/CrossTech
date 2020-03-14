using System.ComponentModel.DataAnnotations.Schema;

namespace CrossTechTask.DAL.Entity
{
    [Table("UserClaims")]
    public class UserClaim
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClaimId { get; set; }
    }
}
