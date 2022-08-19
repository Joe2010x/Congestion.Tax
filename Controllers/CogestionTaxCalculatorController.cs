using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace congestion.calculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxCalculatorController : ControllerBase
    {

        private readonly CongestionTaxCalculator _congestionTaxCalculator;

        public CongestionTaxCalculatorController()
        {
            _congestionTaxCalculator = new CongestionTaxCalculator();
        }

        [HttpPost]
        public int GetTax(SearchDTO searchDTO)
        {
           
           Type t = Type.GetType("congestion.calculator." + searchDTO.vehicleType);
           Vehicle vehicle =  Activator.CreateInstance(t) as Vehicle;

           var dateTimes = new DateTime[searchDTO.dates.Length];
           dateTimes = searchDTO.dates.Select(d => DateTime.Parse(d)).ToArray();

           return _congestionTaxCalculator.GetTax(vehicle,dateTimes);
        }

    }
}
