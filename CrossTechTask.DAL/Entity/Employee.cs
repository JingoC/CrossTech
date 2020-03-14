using CrossTech.ClientApi.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossTechTask.DAL.Entity
{
    [Table("Employees")]
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public SexTypeEnum Sex { get; set; }
        public PositionTypeEnum PositionId { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
    }
}
