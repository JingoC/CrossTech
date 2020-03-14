using CrossTech.ClientApi.Enums;
using System;

namespace CrossTech.ClientApi.Models.Employee
{
    public class EmployeeModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public SexTypeEnum? Sex { get; set; }
        public PositionTypeEnum? PositionId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
    }
}
