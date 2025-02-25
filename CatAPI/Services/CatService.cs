using System;
using System.Threading.Tasks;
using CatApi.Repositories;
using Microsoft.Extensions.Options;
using CatApi.Configurations;
using CatAPI.DTO;

namespace CatApi.Services
{
    public class CatService : ICatService
    {
        private readonly ICatRepository _catRepository;
        private readonly string _catImageBaseUrl;

        public CatService(ICatRepository catRepository, IOptions<ApiSettings> config)
        {
            _catRepository = catRepository;
            _catImageBaseUrl = config.Value.CatImageBaseUrl;
        }

        public async Task<CatData> GetRandomCatDataAsync()
        {
            var fact = await _catRepository.GetRandomCatFactAsync();
            // Append a cache-busting timestamp to the image URL.
            var imageUrl = $"{_catImageBaseUrl}?timestamp={DateTime.Now.Ticks}";
            return new CatData { Fact = fact, ImageUrl = imageUrl };
        }
    }
}