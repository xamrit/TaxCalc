using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaxCalc.API.Extensions;
using TaxCalc.API.Interfaces;

namespace TaxCalc.API
{
    /// <inheritdoc/>
    public class TaxCalculator : ITaxCalculator
    {
        private readonly HttpClient client = new HttpClient();
        private string apiEndpoint;

        public void Initialize(string endpoint, string apiKey)
        {
            apiEndpoint = endpoint;

            const string scheme = "Authorization";
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(scheme, $"Token token=\"{apiKey}\"");
        }

        public async Task<TTaxRate> GetLocationTaxRates<TTaxRate>(string zip, string country = "", string state = "", string city = "", string street = "")
            where TTaxRate : ITaxRate, new()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var baseUri = new UriBuilder($"{apiEndpoint}rates/{zip}");

            // Append parameters as needed if they were assigned.
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
                // Perform GET request.
                var response = await client.GetAsync(baseUri.Uri);
                response.EnsureSuccessStatusCode();
                
                // Deserialize and return tax rate result.
                var taxRateInfo = JsonSerializer.Deserialize<TaxRateInfo<TTaxRate>>(await response.Content.ReadAsStringAsync());
                return taxRateInfo.rate;
            }
            catch (Exception e)
            {
                // If an exception occurs log and throw it.
                Console.WriteLine("\nException in Api.GetLocationTaxRatesForZipCode caught.");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        public async Task<TOrderTax> GetTaxForOrder<TOrderTax>(IOrder order)
            where TOrderTax : IOrderTax, new()
        {
            const string mediaType = "application/json";

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            // Serialize the order payload.
            var orderJson = JsonSerializer.Serialize(order);
            var requestContent = new StringContent(orderJson, Encoding.UTF8, "application/json");

            try
            {
                // Perform POST
                HttpResponseMessage response = await client.PostAsync($"{apiEndpoint}taxes", requestContent);

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        // Deserialize and return response.
                        var responseBody = await streamReader.ReadToEndAsync();
                        var taxRateInfo = JsonSerializer.Deserialize<OrderTaxInfo<TOrderTax>>(responseBody);
                        return taxRateInfo.tax;
                    }
                }
            }
            catch (Exception e)
            {
                // If an exception occurs log and throw it.
                Console.WriteLine("\nException in Api.GetTaxForOrder caught.");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        /// <summary>
        /// Class to assist in deserializing.
        /// </summary>
        private class TaxRateInfo<TTaxRate> where TTaxRate : ITaxRate
        {
            public TTaxRate rate { get; set; }
        }

        /// <summary>
        /// Class to assist in deserializing.
        /// </summary>
        private class OrderTaxInfo<TOrderTax> where TOrderTax : IOrderTax
        {
            public TOrderTax tax { get; set; }
        }
    }
}