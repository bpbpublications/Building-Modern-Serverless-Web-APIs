using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microservices.CountryCapital.Infrastructure.Models;

namespace Microservices.CountryCapital.Infrastructure
{
    public class FileRepository : IRepository
    {
        public async Task<Country> FindCountryByIdAsync(int countryId)
        {
            var content = GetFileData();
            return content.FirstOrDefault(x => x.Id == countryId);
        }

        public async Task<IQueryable<Country>> GetCountriesAsync()
        {
            var content = GetFileData();
            return content.AsQueryable();
        }


        private static List<Country> GetFileData()
        {
            string content = String.Empty;
            var dir = AppContext.BaseDirectory;
            using (var sr = new StreamReader(Path.Combine(dir, "TestFile.txt")))
            {
                content = sr.ReadToEnd();
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(content);

        }
    }
}
