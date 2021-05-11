using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models.OperationResults;

namespace Web.IntegrationTest.Utils.Parsers
{
    internal class HttpResponseMessageParser
    {
        public async Task<TOperationResult> ParseResponseAsync<TOperationResult>(HttpResponseMessage response)
            where TOperationResult : OperationResult
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new NotSupportedException("HttpResponseMessage has unsupported http status code");
            }

            await using Stream responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TOperationResult>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}