using SM = Microservices.CountryCapital.Services.Models;
using System.Collections.Generic;
using Microservices.CountryCapital.Infrastructure;
using IM = Microservices.CountryCapital.Infrastructure.Models;
using System.Threading.Tasks;
using Microservices.CountryCapital.Domain;
using System.Linq;

namespace Microservices.CountryCapital.Services
{
    public class CountryCapitalService : ICountryCapitalService
    {
        private IRepository _repository;
        private IValidateCountryDomain _validateCountryDomain;
        public CountryCapitalService(IRepository repository, IValidateCountryDomain validateCountryDomain)
        {
            _repository = repository;
            _validateCountryDomain = validateCountryDomain;
        }
        public async Task<SM.Country> GetCountryByIdAsync(int countryId)
        {
             IM.Country content = await _repository.FindCountryByIdAsync(countryId);
            SM.Country country = new SM.Country()
            {
                Capital = content.Capital,
                Name=content.Name
            };
            if (content.isValid())
                country.isValid = _validateCountryDomain.Validate(content.Name);
            else
                country.isValid = false;

            return country;
        }

        public async Task<List<SM.Country>> GetCountriesAsync()
        {
            List<SM.Country> countries = new List<SM.Country>();
            IQueryable<IM.Country> contents = await _repository.GetCountriesAsync();
            foreach (var item in contents)
            {
                countries.Add(new SM.Country()
                {
                    Capital = item.Capital,
                    Name=item.Name
                });
            }

            return countries;
        }
    }
}
