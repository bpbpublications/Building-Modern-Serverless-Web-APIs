using Microservices.CountryCapital.Infrastructure.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.CountryCapital.Infrastructure
{
    public interface IRepository
    {
        Task<IQueryable<Country>> GetCountriesAsync();
        Task<Country> FindCountryByIdAsync(int countryId);
    }
}
