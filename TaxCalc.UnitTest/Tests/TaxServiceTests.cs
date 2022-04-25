using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TaxCalc.Core.Services;
using TaxCalc.UnitTest.Mocks;

namespace TaxCalc.UnitTest.Tests
{
    /// <summary>
    /// Tests the application tax service using a <see cref="MockTaxCalculator"/>.
    /// </summary>
    [TestClass]
    public class TaxServiceTests
    {
        /// <summary>
        /// Tests <see cref="TaxService.GetLocationTaxRates(string, string, string, string, string)"/> 
        /// for valid return value.
        /// </summary>
        [TestMethod]
        public async Task TestGetLocationTaxRates()
        {
            var taxService = new TaxService(new MockTaxCalculator());

            var result = await taxService.GetLocationTaxRates("12345");

            Assert.AreEqual(result.combined_rate, "1.0");
        }

        /// <summary>
        /// Tests <see cref="TaxService.GetLocationTaxRates(string, string, string, string, string)"/> for
        /// proper exception handling if exception occurs in the tax calculator. Exception should be thrown.
        /// </summary>
        [TestMethod]
        public void TestGetLocationTaxRatesException()
        {
            var taxService = new TaxService(new MockTaxCalculator());

            Assert.ThrowsExceptionAsync<Exception>(async () => await taxService.GetLocationTaxRates(null));
        }

        /// <summary>
        /// Tests <see cref="TaxService.GetTaxForOrder(Core.Models.Order)"/> for valid return value.
        /// </summary>
        [TestMethod]
        public async Task TestGetTaxForOrder()
        {
            var taxService = new TaxService(new MockTaxCalculator());

            var result = await taxService.GetTaxForOrder(new Core.Models.Order());

            Assert.AreEqual(result.amount_to_collect, 1.0f);
        }

        /// <summary>
        /// Tests <see cref="TaxService.GetTaxForOrder(Core.Models.Order)"/> for
        /// proper exception handling if exception occurs in the tax calculator. Exception should be thrown.
        /// </summary>
        [TestMethod]
        public void TestGetTaxForOrderException()
        {
            var taxService = new TaxService(new MockTaxCalculator());

            Assert.ThrowsExceptionAsync<Exception>(async () => await taxService.GetTaxForOrder(null));
        }
    }
}
