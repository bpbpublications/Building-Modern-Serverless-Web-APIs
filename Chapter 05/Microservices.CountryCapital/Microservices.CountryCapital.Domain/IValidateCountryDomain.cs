using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.CountryCapital.Domain
{
    public interface IValidateCountryDomain
    {
        Boolean Validate(string country);
        Boolean ValidateCapital(string capital, string country);
    }
}
