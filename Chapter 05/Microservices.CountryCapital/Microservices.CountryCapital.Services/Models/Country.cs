using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.CountryCapital.Services.Models
{
    public class Country
    {
        public string Name { get; set; }
        public string  Capital { get; set; }
        [JsonIgnore]
        public Boolean isValid { get; set; }
    }
}
