using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using TaxCalc.Core.ViewModels;
using TaxCalc.UnitTest.Mocks;

namespace TaxCalc.UnitTest.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="TaxRatePageViewModel"/>.
    /// </summary>
    [TestClass]
    public class TaxRatePageViewModelTests
    {
        /// <summary>
        /// Tests if button tap can handle invalid zip.
        /// </summary>
        [TestMethod]
        public void TestOnGetTaxRateButtonCommandInvalidZip()
        {
            var vm = new TaxRatePageViewModel(new MockTaxService());
            vm.Zip = string.Empty;
            vm.TaxLocationResults = "TaxLocationResultsTest";
            vm.TaxRateResults = "TaxRateResultsTest";

            vm.GetTaxRateButtonCommand.Execute(null);

            // Results should not be changed since invalid zip was entered.
            Assert.AreEqual(vm.TaxLocationResults, "TaxLocationResultsTest");
            Assert.AreEqual(vm.TaxRateResults, "TaxRateResultsTest");
        }

        /// <summary>
        /// Tests if button tap can handle tax service exception.
        /// </summary>
        [TestMethod]
        public void TestOnGetTaxRateButtonCommandException()
        {
            var vm = new TaxRatePageViewModel(new MockTaxService());
            vm.Zip = "-1"; // Will trigger exception.
            vm.TaxLocationResults = "TaxLocationResultsTest";
            vm.TaxRateResults = "TaxRateResultsTest";

            vm.GetTaxRateButtonCommand.Execute(null);

            // Results should be changed to no results/empty-string since an exception was thrown.
            Assert.AreEqual(vm.TaxLocationResults, "No Results");
            Assert.AreEqual(vm.TaxRateResults, string.Empty);
        }

        /// <summary>
        /// Tests display text update when all inputs are valid and the button is tapped.
        /// </summary>
        [TestMethod]
        public void TestOnGetTaxRateButtonCommandValid()
        {
            var vm = new TaxRatePageViewModel(new MockTaxService());
            vm.Zip = "12345";
            vm.TaxLocationResults = "TaxLocationResultsTest";
            vm.TaxRateResults = "TaxRateResultsTest";

            vm.GetTaxRateButtonCommand.Execute(null);

            // TaxLocationResults should be changed to the expected display string:
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine("LOCATION");
            builder.AppendLine();
            builder.AppendLine($"Zip: {MockTaxService.MockTaxRateResult.zip}");
            builder.AppendLine($"Country: {MockTaxService.MockTaxRateResult.country}");
            builder.AppendLine($"State: {MockTaxService.MockTaxRateResult.state}");
            builder.AppendLine($"County: {MockTaxService.MockTaxRateResult.county}");
            builder.AppendLine($"City: {MockTaxService.MockTaxRateResult.city}");

            Assert.AreEqual(vm.TaxLocationResults, builder.ToString());


            builder.Clear();

            // TaxRateResults should be changed to the expected display string:
            builder.AppendLine();
            builder.AppendLine("RATES");
            builder.AppendLine();
            builder.AppendLine($"Country Rate: {MockTaxService.MockTaxRateResult.country_rate}");
            builder.AppendLine($"State Rate: {MockTaxService.MockTaxRateResult.state_rate}");
            builder.AppendLine($"County Rate: {MockTaxService.MockTaxRateResult.county_rate}");
            builder.AppendLine($"City Rate: {MockTaxService.MockTaxRateResult.city_rate}");
            builder.AppendLine($"Combined District Rate: {MockTaxService.MockTaxRateResult.combined_district_rate}");
            builder.AppendLine($"Combined Rate: {MockTaxService.MockTaxRateResult.combined_rate}");
            builder.AppendLine($"Freight Taxable: {MockTaxService.MockTaxRateResult.freight_taxable}");

            Assert.AreEqual(vm.TaxRateResults, builder.ToString());
        }
    }
}
