using System.Threading.Tasks;

namespace CatApi.Repositories
{
    public interface ICatRepository
    {
        Task<string> GetRandomCatFactAsync();
    }
}
