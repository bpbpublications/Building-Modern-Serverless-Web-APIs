using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microservices.CountryCapital.Infrastructure.Models;

namespace Microservices.CountryCapital.Infrastructure
{
    public class S3Repository : IRepository
    {
        private static IAmazonS3 client = new AmazonS3Client(RegionEndpoint.USEast1);

        public async Task<Country> FindCountryByIdAsync(int countryId)
        {
            var content = await GetFileDataAsync();
            return content.FirstOrDefault(x => x.Id == countryId);
        }

        public async Task<IQueryable<Country>> GetCountriesAsync()
        {
            var content = await GetFileDataAsync();
            return content.AsQueryable();
        }

        private static async Task<List<Country>> GetFileDataAsync()
        {
            string responseBody = "";
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = "countrycapital",
                    Key = "TestFile.txt"
                };
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {

                    responseBody = reader.ReadToEnd(); // Now you process the response body.
                }
            }
            catch (AmazonS3Exception e)
            {
                // If bucket or object does not exist
                Console.WriteLine("Error encountered ***. Message:'{0}' when reading object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when reading object", e.Message);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(responseBody);

        }
    }
}
