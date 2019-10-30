using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leviathan
{
    public class LeviathanProvider : ILeviathanProvider
    {
        private readonly string _apiUser;
        private readonly string _apiKey;
        private readonly IRestClient _restClient;

        public LeviathanProvider(string apiUser, string apiKey, IRestClient restClient)
        {
            _apiUser = apiUser;
            _apiKey = apiKey;
            _restClient = restClient;
        }

        public async Task<IEnumerable<EmployeeResponse>> GetEmployees()
        {
            var request = new RestRequest("employee", Method.GET);
            request.AddQueryParameter("ApiUser", _apiUser);
            request.AddQueryParameter("ApiKey", _apiKey);

            var result = await _restClient.ExecuteTaskAsync<List<EmployeeResponse>>(request);

            return result.Data;
        }

        public async Task<EmployeeResponse> CreateEmployee(string firstName, string lastName, string telephone, string role)
        {
            var request = new RestRequest("employee", Method.POST);
            request.AddJsonBody(new
            {
                ApiUser = _apiUser,
                ApiKey = _apiKey,
                firstName,
                lastName,
                telephone,
                role
            });

            var result = await _restClient.ExecuteTaskAsync<EmployeeResponse>(request);
            return result.Data;
        }
    }
}
