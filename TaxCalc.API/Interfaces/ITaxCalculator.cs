using System.Threading.Tasks;

namespace TaxCalc.API.Interfaces
{
    /// <summary>
    /// Interface for a tax calculator.
    /// </summary>
    public interface ITaxCalculator
    {
        /// <summary>
        /// Handles tax calculator initializing.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="apiKey">The API key.</param>
        void Initialize(string endpoint, string apiKey);

        /// <summary>
        /// Gets the location tax rates.
        /// </summary>
        /// <typeparam name="TTaxRate">The tax rate object.</typeparam>
        Task<TTaxRate> GetLocationTaxRates<TTaxRate>(string zip, string country = "", string state = "", string city = "", string street = "")
            where TTaxRate : ITaxRate, new();

        /// <summary>
        /// Gets the tax for an order.
        /// </summary>
        /// <typeparam name="TTaxRate">The order tax object.</typeparam>
        Task<TOrderTax> GetTaxForOrder<TOrderTax>(IOrder order)
            where TOrderTax : IOrderTax, new();
    }
}
