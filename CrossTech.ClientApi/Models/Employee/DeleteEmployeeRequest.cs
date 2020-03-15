using System;
using System.Collections.Generic;
using System.Text;

namespace CrossTech.ClientApi.Models.Employee
{
    /// <summary>
    /// Тело запроса на удаление сотрудника
    /// </summary>
    public class DeleteEmployeeRequest : BaseRequest
    {
        /// <summary>
        /// Id удаляемого сотрудника
        /// </summary>
        public int Id { get; set; }
    }
}
