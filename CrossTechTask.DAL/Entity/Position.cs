using System.ComponentModel.DataAnnotations.Schema;

namespace CrossTechTask.DAL.Entity
{
    [Table("Positions")]
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
