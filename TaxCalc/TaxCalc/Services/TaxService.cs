using System.Threading.Tasks;
using TaxCalc.Core.Models;
using TaxCalc.API.Interfaces;
using TaxCalc.API;

namespace TaxCalc.Core.Services
{
    /// <inheritdoc/>
    public class TaxService : ITaxService
    {
        private ITaxCalculator _taxCalculator;

        /// <summary>
        /// Default constructor that uses default <see cref="TaxCalculator"/> type.
        /// </summary>
        public TaxService() : this(new TaxCalculator()) { }

        /// <summary>
        /// Constructor allowing for custom <see cref="ITaxCalculator"/>.
        /// </summary>
        /// <param name="taxCalculator">The custom implementation of the <see cref="ITaxCalculator"/>.</param>
        public TaxService(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
            Initialize();
        }

        public void Initialize() =>
            _taxCalculator.Initialize("https://api.taxjar.com/v2/", "5da2f821eee4035db4771edab942a4cc");

        public async Task<TaxRate> GetLocationTaxRates(string zip, string country = "", string state = "", string city = "", string street = "")
        {
            try
            {
                return await _taxCalculator.GetLocationTaxRates<TaxRate>(zip, country, state, city, street);
            }
            catch
            {
                throw; // Allow caller to handle exception.
            }
        }

        public async Task<OrderTax> GetTaxForOrder(Order order)
        {
            try
            {
                return await _taxCalculator.GetTaxForOrder<OrderTax>(order);
            }
            catch
            {
                throw; // Allow caller to handle exception.
            }
        }
    }
}
