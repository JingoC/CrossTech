using CrossTech.ClientApi.Models.Position;
using System.Collections.Generic;

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
