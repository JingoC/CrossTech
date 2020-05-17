using System.ComponentModel.DataAnnotations.Schema;

namespace CrossTechTask.DAL.Entity
{
    [Table("Claims")]
    public class Claim
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
