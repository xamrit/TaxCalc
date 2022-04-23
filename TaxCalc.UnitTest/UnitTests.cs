using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using TaxCalc.API;
using TaxCalc.API.Interfaces;

namespace TaxCalc.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async void TestMethod()
        {
            //Api.Initialize("https://api.taxjar.com/v2/", "5da2f821eee4035db4771edab942a4cc");
            //var order = "{\n    \"from_country\": \"US\",\n    \"from_zip\": \"07001\",\n    \"from_state\": \"NJ\",\n    \"to_country\": \"US\",\n    \"to_zip\": \"07446\",\n    \"to_state\": \"NJ\",\n    \"amount\": 16.50,\n    \"shipping\": 1.5,\n    \"line_items\": [\n        {\n            \"quantity\": 1,\n            \"unit_price\": 15.0,\n            \"product_tax_code\": \"31000\"\n        }\n    ]\n}";

            //await Api.GetTaxForOrder(order);
        }

        public class Order : IOrder
        {
            public string from_country { get; set; }
            public string from_zip { get; set; }
            public string from_state { get; set; }
            public string from_city { get; set; }
            public string from_street { get; set; }
            public string to_country { get; set; }
            public string to_zip { get; set; }
            public string to_state { get; set; }
            public string to_city { get; set; }
            public string to_street { get; set; }
            public float amount { get; set; }
            public float shipping { get; set; }
        }
    }
}
