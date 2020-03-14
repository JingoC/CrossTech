using CrossTech.ClientApi.Models;
using CrossTech.ClientApi.Models.Employee;
using CrossTech.Core.Service;
using System.Threading.Tasks;

namespace CrossTech.ClientApi.Service
{
    public interface IEmployeeRemoteCallService : IBaseRemoteCallService
    {
        Task<BaseResponse> Add(AddEmployeeRequest request);
        Task<BaseResponse> Update(UpdateEmployeeRequest request);
        Task<GetEmployeesResponse> Get(BaseRequest request);
        Task<BaseResponse> Delete(DeleteEmployeeRequest request);

        Task<GetPositionsResponse> GetPositions(BaseRequest request);
    }
}
