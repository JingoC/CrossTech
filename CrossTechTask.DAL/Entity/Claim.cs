using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CrossTechTask.DAL.Entity
{
    [Table("Claims")]
    public class Claim
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
