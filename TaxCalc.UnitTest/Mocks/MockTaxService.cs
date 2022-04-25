using System;
using System.Threading.Tasks;
using TaxCalc.Core.Models;
using TaxCalc.Core.Services;

namespace TaxCalc.UnitTest.Mocks
{
    /// <summary>
    /// Mock tax service for unit testing purposes.
    /// </summary>
    internal class MockTaxService : ITaxService
    {
        /// <summary>
        /// The dummy returned tax rate result.
        /// </summary>
        public static TaxRate MockTaxRateResult = new TaxRate()
        {
            zip = "12345",
            country = "US",
            state = "VA",
            county = "Fairfax",
            city = "Fairfax",
            country_rate = "0.1",
            state_rate = "0.2",
            county_rate = "0.3",
            city_rate = "0.4",
            combined_district_rate = "0.5",
            combined_rate = "0.6",
            freight_taxable = true,
        };

        /// <summary>
        /// The dummy returned order tax result.
        /// </summary>
        public static OrderTax MockOrderTaxResult = new OrderTax()
        {
            amount_to_collect = 1f,
            order_total_amount = 2f,
            shipping = 3f,
            taxable_amount = 4f,
            rate = 5f,
            has_nexus = true,
            freight_taxable = false,
            tax_source = "tax_source",
            exemption_type = "exemption_type",
        };

        /// <summary>
        /// Returns a dummy location tax object (<see cref="MockTaxRateResult"/>). 
        /// Provides a means to create an exception for testing purposes.
        /// </summary>
        public Task<TaxRate> GetLocationTaxRates(string zip, string country = "", string state = "", string city = "", string street = "")
        {
            if (zip == "-1")
                throw new Exception();

            return Task.FromResult(MockTaxRateResult);
        }

        /// <summary>
        /// Returns a dummy order tax object (<see cref="MockTaxRateResult"/>). 
        /// Provides a means to create an exception for testing purposes.
        /// </summary>
        public Task<OrderTax> GetTaxForOrder(Order order)
        {
            if (order.to_country == "--")
                throw new Exception();

            return Task.FromResult(MockOrderTaxResult);
        }

        /// <summary>
        /// Mock initialization (does nothing).
        /// </summary>
        public void Initialize()
        {
            
        }
    }
}