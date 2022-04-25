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

        public static async Task<TTaxRate> GetLocationTaxRatesForZipCode<TTaxRate>(string zip, string country = "", string state = "", string city = "", string street = "")
            where TTaxRate : ITaxRate
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
                var taxRateInfo = JsonSerializer.Deserialize<TaxRateInfo<TTaxRate>>(await response.Content.ReadAsStringAsync());
                return taxRateInfo.rate;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException in Api.GetLocationTaxRatesForZipCode caught.");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        public static async Task<TOrderTax> GetTaxForOrder<TOrderTax>(IOrder order)
            where TOrderTax : IOrderTax
        {
            const string mediaType = "application/json";

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            var orderJson = JsonSerializer.Serialize(order);
            var requestContent = new StringContent(orderJson, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await Client.PostAsync($"{ApiEndpoint}taxes", requestContent);

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var responseBody = await streamReader.ReadToEndAsync();
                        var taxRateInfo = JsonSerializer.Deserialize<OrderTaxInfo<TOrderTax>>(responseBody);
                        return taxRateInfo.tax;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException in Api.GetTaxForOrder caught.");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }
    }

    internal class TaxRateInfo<TTaxRate> where TTaxRate : ITaxRate
    {
        public TTaxRate rate { get; set; }
    }

    internal class OrderTaxInfo<TOrderTax> where TOrderTax : IOrderTax
    {
        public TOrderTax tax { get; set; }
    }
}