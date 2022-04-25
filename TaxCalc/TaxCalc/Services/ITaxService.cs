using System.Threading.Tasks;
using TaxCalc.Core.Models;

namespace TaxCalc.Core.Services
{
    /// <summary>
    /// The tax service.
    /// </summary>
    public interface ITaxService
    {
        /// <summary>
        /// Initializes the tax service.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Gets the location tax rates.
        /// </summary>
        Task<TaxRate> GetLocationTaxRates(string zip, string country = "", string state = "", string city = "", string street = "");
        
        /// <summary>
        /// Gets the tax for an order.
        /// </summary>
        Task<OrderTax> GetTaxForOrder(Order order);
    }
}