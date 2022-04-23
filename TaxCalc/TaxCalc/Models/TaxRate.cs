using TaxCalc.API.Interfaces;

namespace TaxCalc.Models
{
    public class TaxRate : ITaxRate
    {
        public string zip { get; set; }
        public string country { get; set; }
        public float country_rate { get; set; }
        public string state { get; set; }
        public float state_rate { get; set; }
        public string county { get; set; }
        public float county_rate { get; set; }
        public string city { get; set; }
        public float city_rate { get; set; }
        public float combined_district_rate { get; set; }
        public float combined_rate { get; set; }
        public float freight_taxable { get; set; }
    }
}
