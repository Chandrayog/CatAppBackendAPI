using System.Threading.Tasks;
using CatAPI.DTO;

namespace CatApi.Services
{
    public interface ICatService
    {
        Task<CatData> GetRandomCatDataAsync();
    }
}
