using System;
using System.Collections.Generic;
using System.Text;

namespace CrossTech.ClientApi.Models.Employee
{
    /// <summary>
    /// Тело ответа на запрос списка получения должностей
    /// </summary>
    public class GetPositionsResponse : BaseResponse
    {
        /// <summary>
        /// Список должностей
        /// </summary>
        public List<PositionModel> Positions { get; set; }
    }
}
