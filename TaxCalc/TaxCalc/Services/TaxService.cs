using System.Threading.Tasks;
using TaxCalc.Core.Models;
using TaxCalc.API;
namespace TaxCalc.Core.Services
{
    public class TaxService : ITaxService
    {
        public void Initialize() =>
            Api.Initialize("https://api.taxjar.com/v2/", "5da2f821eee4035db4771edab942a4cc");

        public async Task<TaxRate> GetLocationTaxRatesForZipCode(string zip, string country = "", string state = "", string city = "", string street = "")
        {
            try
            {
                return await Api.GetLocationTaxRatesForZipCode<TaxRate>(zip, country, state, city, street);
            }
            catch
            {
                throw;
            }
        }

        public async Task<OrderTax> GetTaxForOrder(Order order)
        {
            return null; // TODO
        }
    }
}
