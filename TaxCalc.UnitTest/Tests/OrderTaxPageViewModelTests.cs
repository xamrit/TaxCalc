using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using TaxCalc.Core.ViewModels;
using TaxCalc.UnitTest.Mocks;

namespace TaxCalc.UnitTest.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="OrderTaxPageViewModel"/>.
    /// </summary>
    [TestClass]
    public class OrderTaxPageViewModelTests
    {
        /// <summary>
        /// Tests invalid and valid inputs when the button is tapped. Progresses towards
        /// more valid inputs.
        /// </summary>
        [TestMethod]
        public void TestOnGetOrderTaxButtonCommandInput()
        {
            const string OrderTaxResultsTest = "OrderTaxResultsTest";
            var vm = new OrderTaxPageViewModel(new MockTaxService());
            vm.OrderTaxResults = OrderTaxResultsTest;

            // Test no valid inputs - some valid inputs. OrderTaxResults should not change.
            vm.GetOrderTaxButtonCommand.Execute(null);
            Assert.AreEqual(vm.OrderTaxResults, OrderTaxResultsTest, message: "Invalid input: OrderTaxResults should not have changed.");

            vm.Amount = "1";
            vm.GetOrderTaxButtonCommand.Execute(null);
            Assert.AreEqual(vm.OrderTaxResults, OrderTaxResultsTest, message: "Invalid input: OrderTaxResults should not have changed."); ;

            vm.Shipping = "1";
            vm.GetOrderTaxButtonCommand.Execute(null);
            Assert.AreEqual(vm.OrderTaxResults, OrderTaxResultsTest, message: "Invalid input: OrderTaxResults should not have changed.");

            vm.ToCountry = "US";
            vm.GetOrderTaxButtonCommand.Execute(null);
            Assert.AreEqual(vm.OrderTaxResults, OrderTaxResultsTest, message: "Invalid input: OrderTaxResults should not have changed.");

            vm.ToState = "VA";
            vm.GetOrderTaxButtonCommand.Execute(null);
            Assert.AreEqual(vm.OrderTaxResults, OrderTaxResultsTest, message: "Invalid input: OrderTaxResults should not have changed.");

            vm.ToZip = "12345";

            // All inputs valid. OrderTaxResults should change.
            vm.GetOrderTaxButtonCommand.Execute(null);
            Assert.AreNotEqual(vm.OrderTaxResults, OrderTaxResultsTest, message: "Valid input: OrderTaxResults should have changed.");
        }

        /// <summary>
        /// Tests proper display update when tapping the button.
        /// </summary>
        [TestMethod]
        public void TestOnGetOrderTaxButtonCommandDisplay()
        {
            const string OrderTaxResultsTest = "OrderTaxResultsTest";

            var vm = new OrderTaxPageViewModel(new MockTaxService());
            vm.OrderTaxResults = OrderTaxResultsTest;

            vm.Amount = "1";
            vm.Shipping = "1";
            vm.ToCountry = "US";
            vm.ToState = "VA";
            vm.ToZip = "12345";

            // Valid inputs, display should fully update to show result.
            vm.GetOrderTaxButtonCommand.Execute(null);


            var builder = new StringBuilder();

            var hasNexus = MockTaxService.MockOrderTaxResult.has_nexus ? "Yes" : "No";
            var freightTaxable = MockTaxService.MockOrderTaxResult.freight_taxable ? "Yes" : "No";

            builder.AppendLine($"Order Total Amount: {MockTaxService.MockOrderTaxResult.order_total_amount:C}");
            builder.AppendLine($"Shipping: {MockTaxService.MockOrderTaxResult.shipping:C}");
            builder.AppendLine($"Taxible Amount: {MockTaxService.MockOrderTaxResult.taxable_amount:C}");
            builder.AppendLine($"Amount to collect: {MockTaxService.MockOrderTaxResult.amount_to_collect:C}");
            builder.AppendLine($"Rate: {MockTaxService.MockOrderTaxResult.rate:C}");
            builder.AppendLine($"Tax Source: {MockTaxService.MockOrderTaxResult.tax_source}");
            builder.AppendLine($"Exemption Type: {MockTaxService.MockOrderTaxResult.exemption_type}");
            builder.AppendLine($"Has Nexus?: {hasNexus}");
            builder.AppendLine($"Freight Taxible?: {freightTaxable}");

            Assert.AreEqual(vm.OrderTaxResults, builder.ToString(), message: "OrderTaxResults text should update to display full tax info.");
        }

        /// <summary>
        /// Tests if a button tap can properly handle an exception thrown
        /// by the tax service.
        /// </summary>
        [TestMethod]
        public void TestOnGetOrderTaxButtonCommandException()
        {
            const string OrderTaxResultsTest = "OrderTaxResultsTest";

            var vm = new OrderTaxPageViewModel(new MockTaxService());
            vm.OrderTaxResults = OrderTaxResultsTest;

            vm.Amount = "1";
            vm.Shipping = "1";
            vm.ToCountry = "--"; // Should trigger exception.
            vm.ToState = "VA";
            vm.ToZip = "12345";

            vm.GetOrderTaxButtonCommand.Execute(null);

            Assert.AreEqual(vm.OrderTaxResults, "No Results", message: "Exception occured, OrderTaxResults should have been updated to be 'No Results'.");
        }
    }
}
