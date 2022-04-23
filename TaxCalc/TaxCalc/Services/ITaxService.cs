using System.Threading.Tasks;
using TaxCalc.Core.Models;

namespace TaxCalc.Core.Services
{
    public interface ITaxService
    {
        void Initialize();

        Task<TaxRate> GetLocationTaxRatesForZipCode(string zip, string country = "", string state = "", string city = "", string street = "");
        Task<OrderTax> GetTaxForOrder(Order order);
    }
}