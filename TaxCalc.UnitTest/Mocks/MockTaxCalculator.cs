using System;
using System.Threading.Tasks;
using TaxCalc.API.Interfaces;

namespace TaxCalc.UnitTest.Mocks
{
    /// <summary>
    /// Mock tax calculator for unit testing purposes.
    /// </summary>
    internal class MockTaxCalculator : ITaxCalculator
    {
        /// <summary>
        /// Returns a dummy tax rate object. Provides a means to create an exception for testing purposes.
        /// </summary>
        public Task<TTaxRate> GetLocationTaxRates<TTaxRate>(string zip, string country = "", string state = "", string city = "", string street = "") where TTaxRate : ITaxRate, new()
        {
            if (zip == null)
                throw new Exception();

            return Task.FromResult(new TTaxRate()
            {
                combined_rate = "1.0",
            });
        }

        /// <summary>
        /// Returns a dummy order tax object. Provides a means to create an exception for testing purposes.
        /// </summary>
        public Task<TOrderTax> GetTaxForOrder<TOrderTax>(IOrder order) where TOrderTax : IOrderTax, new()
        {
            if (order == null)
                throw new Exception();

            return Task.FromResult(new TOrderTax()
            {
                amount_to_collect = 1.0f,
            });
        }

        /// <summary>
        /// Mock initialization (does nothing).
        /// </summary>
        public void Initialize(string endpoint, string apiKey)
        {

        }
    }
}
