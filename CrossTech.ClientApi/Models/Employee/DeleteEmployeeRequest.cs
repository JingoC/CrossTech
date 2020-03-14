using System;
using System.Collections.Generic;
using System.Text;

namespace CrossTech.ClientApi.Models.Employee
{
    public class DeleteEmployeeRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}
