using CrossTech.ClientApi.Enums;
using System;

namespace CrossTech.ClientApi.Models.Employee
{
    /// <summary>
    /// Модель сотрудника
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string FatherName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public SexTypeEnum? Sex { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public PositionTypeEnum? PositionId { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
    }
}
