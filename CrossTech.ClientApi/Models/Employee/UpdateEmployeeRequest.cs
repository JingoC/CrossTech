namespace CrossTech.ClientApi.Models.Employee
{
    /// <summary>
    /// Тело запроса на обновление сотрудника
    /// </summary>
    public class UpdateEmployeeRequest : BaseRequest
    {
        /// <summary>
        /// Сотрудник
        /// </summary>
        public EmployeeModel Employee { get; set; }
    }
}
