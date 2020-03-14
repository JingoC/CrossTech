using System.Collections.Generic;

namespace CrossTech.ClientApi.Models.Employee
{
    public class GetEmployeesResponse : BaseResponse
    {
        public List<EmployeeModel> Employees { get; set; }
    }
}
