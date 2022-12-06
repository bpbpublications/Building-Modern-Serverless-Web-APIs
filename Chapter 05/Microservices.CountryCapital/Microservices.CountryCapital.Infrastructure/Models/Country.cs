using System;

namespace Microservices.CountryCapital.Infrastructure.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Capital { get; set; }

        public Boolean isValid()
        {
            return Id != 0;
        }
    }
}
