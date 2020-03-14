using CrossTech.ClientApi.Models;
using CrossTech.ClientApi.Models.Employee;
using CrossTech.Core.Providers;
using CrossTech.Core.Service.Implementation;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CrossTech.ClientApi.Service.Implementation
{
    public class EmployeeRemoteCallService : BaseRemoteCallService, IEmployeeRemoteCallService
    {
        protected override string _apiSchemeAndHostConfigKey { get; set; } = "Host.CrossTech.WebApi";
        public EmployeeRemoteCallService(
            IConfiguration configuration, 
            IWebRequestProvider webRequestProvider
            ):base(configuration, webRequestProvider)
        {

        }

        public async Task<BaseResponse> Add(AddEmployeeRequest request)
            => await ExecutePostAsync<BaseResponse, AddEmployeeRequest>("api/employee/add", request);
        public async Task<BaseResponse> Update(UpdateEmployeeRequest request)
            => await ExecutePostAsync<BaseResponse, UpdateEmployeeRequest>("api/employee/update", request);
        public async Task<GetEmployeesResponse> Get(BaseRequest request)
            => await ExecutePostAsync<GetEmployeesResponse, BaseRequest>("api/employee/get", request);
        public async Task<BaseResponse> Delete(DeleteEmployeeRequest request)
            => await ExecutePostAsync<BaseResponse, DeleteEmployeeRequest>("api/employee/delete", request);

        public async Task<GetPositionsResponse> GetPositions(BaseRequest request)
            => await ExecutePostAsync<GetPositionsResponse, BaseRequest>("api/employee/get-positions", request);
    }
}
