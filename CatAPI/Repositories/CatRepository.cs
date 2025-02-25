using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using CatApi.Configurations;

namespace CatApi.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _catFactApiUrl;

        public CatRepository(IHttpClientFactory clientFactory, IOptions<ApiSettings> config)
        {
            _clientFactory = clientFactory;
            _catFactApiUrl = config.Value.CatFactApiUrl;
        }

        public async Task<string> GetRandomCatFactAsync()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetStringAsync(_catFactApiUrl);
                var json = JObject.Parse(response);
                return json["fact"]?.ToString() ?? "No cat fact available.";
            }
            catch (Exception)
            {
                // Optionally log the error here
                return "Unable to retrieve cat fact at this time.";
            }
        }
    }
}