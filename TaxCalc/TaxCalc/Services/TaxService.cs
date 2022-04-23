using System.Threading.Tasks;
using TaxCalc.Models;
using TaxCalc.API;
using System.Text.Json;

namespace TaxCalc.Services
{
    public class TaxService : ITaxService
    {
        public void Initialize() =>
            Api.Initialize("https://api.taxjar.com/v2/", "5da2f821eee4035db4771edab942a4cc");

        public async Task<TaxRate> GetLocationTaxRatesForZipCode(string zip, string country = "", string state = "", string city = "", string street = "")
        {
            var rateJson = await Api.GetLocationTaxRatesForZipCode(zip, country, state, city, street);
            return JsonSerializer.Deserialize<TaxRate>(rateJson);
        }

        public async Task<OrderTax> GetTaxForOrder(Order order)
        {
            var orderJson = JsonSerializer.Serialize(order);
            var rateJson = await Api.GetTaxForOrder(orderJson);
            return JsonSerializer.Deserialize<OrderTax>(rateJson);
        }
    }
}
