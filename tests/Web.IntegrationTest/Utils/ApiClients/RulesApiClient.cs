using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal class RulesApiClient : ApiClientBase
    {
        public RulesApiClient(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
            : base(httpClient, httpResponseMessageParser)
        {
        }

        protected override string ApiBaseUrl => "api/RulesApi/";

        public async Task<OperationResult<CreateRuleResponse>> CreateAsync(CreateRuleRequest request)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "Create")
            {
                Content = JsonContent.Create(request)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult<CreateRuleResponse>>(response);
        }

        public async Task<OperationResult<GetAllFromWorkspaceResponse>> GetAllFromWorkspaceAsync(GetAllFromWorkspaceRequest request)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, ApiBaseUrl + "GetAllFromWorkspace")
            {
                Content = JsonContent.Create(request)
            });
            return await HttpResponseMessageParser.ParseResponseAsync<OperationResult<GetAllFromWorkspaceResponse>>(response);
        }
    }
}