using System;
using System.Collections.Generic;

namespace Microservices.CountryCapital.Domain
{
    public class ValidateCountryDomain : IValidateCountryDomain
    {
        List<string> ValidCountries = new List<string>() { "UnitesStates", "India", "Russia", "Japan" };
        Dictionary<string, string> ValidateCapitals = new Dictionary<string, string>() {
            { "UnitedStates", "Washington" },
            { "India", "NewDelhi" },
            {"Russia","Moscow" },
            {"Japan","Tokyo" }
        };
        public Boolean Validate(string country)
        {
            return ValidCountries.Contains(country);
        }

        public Boolean ValidateCapital(string capital, string country)
        {
            return ValidateCapitals[country].Equals(capital);
        }

    }
}
