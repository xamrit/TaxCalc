using TaxCalc.API.Interfaces;

namespace TaxCalc.Core.Models
{
    public class Order : IOrder
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string from_city { get; set; }
        public string from_street { get; set; }
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public string to_city { get; set; }
        public string to_street { get; set; }
        public float amount { get; set; }
        public float shipping { get; set; }
    }
}
