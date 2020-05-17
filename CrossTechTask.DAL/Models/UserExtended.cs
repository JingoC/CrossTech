using CrossTechTask.DAL.Entity;
using System.Collections.Generic;

namespace CrossTechTask.DAL.Models
{
    public class UserExtended
    {
        public User User { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
