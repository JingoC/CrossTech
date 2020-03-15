using System.Collections.Generic;

namespace CrossTech.ClientApi.Models.Employee
{
    /// <summary>
    /// Тело ответа на запрос получения сотрудников
    /// </summary>
    public class GetEmployeesResponse : BaseResponse
    {
        /// <summary>
        /// Список сотрудников
        /// </summary>
        public List<EmployeeModel> Employees { get; set; }
    }
}
