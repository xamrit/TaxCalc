using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TaxCalc.API.Extensions;

namespace TaxCalc.API
{
    public static class Api
    {
        private static readonly HttpClient Client = new HttpClient();
        private static string ApiEndpoint;

        public static void Initialize(string endpoint, string apiKey)
        {
            ApiEndpoint = endpoint;

            const string scheme = "Authorization";
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(scheme, $"Token token=\"{apiKey}\"");
        }

        //static async Task Main()
        //{
        //    Initialize("https://api.taxjar.com/v2/", "5da2f821eee4035db4771edab942a4cc");
        //    //await GetLocationTaxRatesForZipCode("98109", country:"US",city:"Seattle", street: "400 Broad Street");


        //    var order = "{\n    \"from_country\": \"US\",\n    \"from_zip\": \"07001\",\n    \"from_state\": \"NJ\",\n    \"to_country\": \"US\",\n    \"to_zip\": \"07446\",\n    \"to_state\": \"NJ\",\n    \"amount\": 16.50,\n    \"shipping\": 1.5,\n    \"line_items\": [\n        {\n            \"quantity\": 1,\n            \"unit_price\": 15.0,\n            \"product_tax_code\": \"31000\"\n        }\n    ]\n}";

        //    var res = await GetTaxForOrder(order);
        //    Debug.WriteLine(res);
        //}

        public static async Task<string> GetLocationTaxRatesForZipCode(string zip, string country = "", string state = "", string city = "", string street = "")
        {
            Client.DefaultRequestHeaders.Accept.Clear();

            var baseUri = new UriBuilder($"{ApiEndpoint}rates/{zip}");

            if (!string.IsNullOrWhiteSpace(country))
                baseUri.AppendParameter($"country={country}");
            if (!string.IsNullOrWhiteSpace(state))
                baseUri.AppendParameter($"state={state}");
            if (!string.IsNullOrWhiteSpace(city))
                baseUri.AppendParameter($"city={city}");
            if (!string.IsNullOrWhiteSpace(street))
                baseUri.AppendParameter($"street={street}");

            try
            {
                var response = await Client.GetAsync(baseUri.Uri);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                Debug.WriteLine(baseUri.Uri.ToString());
                Debug.WriteLine(responseBody);
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message :{0} ", e.Message);
            }

            return null;
        }

        public static async Task<string> GetTaxForOrder(string orderJson)
        {
            const string mediaType = "application/json";

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            var buffer = System.Text.Encoding.UTF8.GetBytes(orderJson);
            var byteContent = new ByteArrayContent(buffer);

            try
            {
                HttpResponseMessage response = await Client.PostAsync($"{ApiEndpoint}taxes", byteContent);

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message :{0} ", e.Message); 
            }

            return null; 
        }
    }
}