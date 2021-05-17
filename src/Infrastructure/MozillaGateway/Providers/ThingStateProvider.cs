using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Domain.Entities.WebThingsGateway.Things;

namespace PolytechWebThings.Infrastructure.MozillaGateway.Providers
{
    internal class ThingStateProvider : IThingStateProvider
    {
        private readonly HttpClient httpClient;

        public ThingStateProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient(nameof(HttpClient));
        }

        public async Task<ThingState> GetStateAsync(Thing thing)
        {
            string url = thing.Links.First(link => link.Rel == "properties").Href;
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)
            {
                Headers =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", thing.Workspace.AccessToken)
                }
            });
            throw new NotImplementedException();
        }
    }
}