namespace CrossTech.ClientApi.Models.Employee
{
    /// <summary>
    /// Тело запроса на добавление сотрудника
    /// </summary>
    public class AddEmployeeRequest : BaseRequest
    {
        /// <summary>
        /// Сотрудник
        /// </summary>
        public EmployeeModel Employee { get; set; }
    }
}
