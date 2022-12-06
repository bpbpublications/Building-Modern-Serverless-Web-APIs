using Microservices.CountryCapital.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.CountryCapital.Services
{
    public interface ICountryCapitalService
    {
        Task<Country> GetCountryByIdAsync(int countryId);
        Task<List<Country>> GetCountriesAsync();
    }
}
