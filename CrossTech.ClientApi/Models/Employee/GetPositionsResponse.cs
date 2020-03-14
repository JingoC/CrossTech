using System;
using System.Collections.Generic;
using System.Text;

namespace CrossTech.ClientApi.Models.Employee
{
    public class GetPositionsResponse : BaseResponse
    {
        public List<PositionModel> Positions { get; set; }
    }
}
