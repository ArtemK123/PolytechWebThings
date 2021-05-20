using System.Net.Http;
using Web.IntegrationTest.Utils.Parsers;

namespace Web.IntegrationTest.Utils.ApiClients
{
    internal abstract class ApiClientBase
    {
        protected ApiClientBase(HttpClient httpClient, HttpResponseMessageParser httpResponseMessageParser)
        {
            HttpClient = httpClient;
            HttpResponseMessageParser = httpResponseMessageParser;
        }

        protected abstract string ApiBaseUrl { get; }

        protected HttpClient HttpClient { get; }

        protected HttpResponseMessageParser HttpResponseMessageParser { get; }
    }
}