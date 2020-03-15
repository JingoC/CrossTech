using System;
using System.Collections.Generic;
using System.Text;

namespace CrossTech.ClientApi.Models.Employee
{
    /// <summary>
    /// Модель должности
    /// </summary>
    public class PositionModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
