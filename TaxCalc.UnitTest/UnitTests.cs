using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TaxCalc.API;
using TaxCalc.API.Interfaces;
using TaxCalc.Core.Services;
using TaxCalc.Core.ViewModels;

namespace TaxCalc.UnitTest
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public async Task TestMethod()
        {
            //Api.Initialize("https://api.taxjar.com/v2/", "5da2f821eee4035db4771edab942a4cc");
            //await Api.GetTaxForOrder<OrderTax>(new Order());
            //;
            //var order = "{\n    \"from_country\": \"US\",\n    \"from_zip\": \"07001\",\n    \"from_state\": \"NJ\",\n    \"to_country\": \"US\",\n    \"to_zip\": \"07446\",\n    \"to_state\": \"NJ\",\n    \"amount\": 16.50,\n    \"shipping\": 1.5,\n    \"line_items\": [\n        {\n            \"quantity\": 1,\n            \"unit_price\": 15.0,\n            \"product_tax_code\": \"31000\"\n        }\n    ]\n}";

            //Api.Initialize("https://api.taxjar.com/v2/", "5da2f821eee4035db4771edab942a4cc");
            //var res = await Api.GetLocationTaxRatesForZipCode("12345");

            //var service = new TaxService();
            //service.Initialize();

            //var vm = new TaxRatePageViewModel(service);
            //vm.Zip = "12345";
            //vm.GetTaxRateButtonCommand.Execute(null);
        }
    }
}
