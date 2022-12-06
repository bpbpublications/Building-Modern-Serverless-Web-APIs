using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.CountryCapital.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.CountryCapital.API.Controllers
{
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        ICountryCapitalService _countryCapitalService;
        public CountryController(ICountryCapitalService countryCapitalService)
        {
            _countryCapitalService = countryCapitalService;
        }

        [HttpGet]
        public async Task<IActionResult> AllAsync()
        {
            var countrylist = await _countryCapitalService.GetCountriesAsync();
            return Ok(countrylist);
        }

        [HttpGet("{id}",Name = "GetByCountryId")]
        public async Task<IActionResult> GetByCountryId(int id)
        {
            var country = await _countryCapitalService.GetCountryByIdAsync(id);
            if (country.isValid)
                return Ok(country);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Not a valid country");
        }
    }
}