using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using TaxCalc.Calculators.TaxJar.Model;
using TaxCalc.Exceptions;
using TaxCalc.Interfaces;
using TaxCalc.Model;

namespace TaxCalc.Calculators.TaxJar
{
    public class TaxJarCalculator : ITaxCalculator
    {
        private readonly string _apiUrl = "https://api.taxjar.com/v2";
        private readonly string _apiKey;
        private readonly HttpClient _client;
        private readonly Order _order;

        // If this code is going to be used in an ASP.NET project then using IHttpClientFactory could be a better choice for centralized HttpClient management
        public TaxJarCalculator(string apiKey, HttpClient client)
        {
            this._apiKey = apiKey;
            this._client = client;
            this._client.DefaultRequestHeaders.Add("Authorization", $"Token token={this._apiKey}");
            this._order = new Order();
        }

        async Task<double> ITaxCalculator.CalculateTaxesForOrder()
        {
            var taxBreakdownRequestUrl = $"{this._apiUrl}/taxes";
            using var taxBreakdownRequest = new HttpRequestMessage(HttpMethod.Get, taxBreakdownRequestUrl);
            var taxBreakdownRequestBody = new TaxesRequest(this._order).ToJson();

            using var taxBreakdownResponse = await this._client.PostAsync(taxBreakdownRequestUrl, new StringContent(taxBreakdownRequestBody, Encoding.UTF8, "application/json"));
            var taxBreakdownResponseBody = await taxBreakdownResponse.Content.ReadAsStringAsync();
            var taxBreakdown = JsonSerializer.Deserialize<TaxesResponse>(taxBreakdownResponseBody);

            if (taxBreakdown.Tax.Breakdown == null)
            {
                throw new InvalidOrderException();
            }

            return taxBreakdown.Tax.AmountToCollect;
        }

        void ITaxCalculator.SetToAddress(Address address)
        {
            this._order.ToAddress = address;
        }

        void ITaxCalculator.SetFromAddress(Address address)
        {
            this._order.FromAddress = address;
        }

        void ITaxCalculator.SetShipping(double shipping)
        {
            this._order.Shipping = shipping;
        }

        void ITaxCalculator.AddOrderProduct(OrderProduct orderProduct)
        {
            this._order.OrderProducts.Add(orderProduct);
        }

        void ITaxCalculator.RemoveOrderProduct(int index)
        {
            this._order.OrderProducts.RemoveAt(index);
        }

        void ITaxCalculator.ClearOrderProducts()
        {
            this._order.OrderProducts.Clear();
        }

        void ITaxCalculator.AddNexusAddress(Address address)
        {
            this._order.NexusAddresses.Add(address);
        }

        void ITaxCalculator.ClearNexusAddresses()
        {
            this._order.NexusAddresses.Clear();
        }

        async Task<LocationTaxRates> ITaxCalculator.GetTaxRateForToAddress() =>
            await this.GetLocationTaxRates(this._order.ToAddress);

        async Task<LocationTaxRates> ITaxCalculator.GetTaxRateForFromAddress() =>
            await this.GetLocationTaxRates(this._order.FromAddress);


        private async Task<LocationTaxRates> GetLocationTaxRates(Address address)
        {
            if (string.IsNullOrEmpty(address?.Zip))
            {
                throw new ArgumentException("Zip code must be set");
            }

            var requestUrl = this.BuildTaxRateForLocationUrl(address, false);
            using var response = await this._client.GetAsync(requestUrl);

            var body = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<TaxJarLocationRateRoot>(body);

            return new LocationTaxRates(root.TaxRate);
        }

        private string BuildTaxRateForLocationUrl(Address address, bool encodeParameters = true)
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(address.City))
            {
                parameters["city"] = address.City;
            }
            if (!string.IsNullOrWhiteSpace(address.State))
            {
                parameters["state"] = address.State;
            }
            if (!string.IsNullOrWhiteSpace(address.County))
            {
                parameters["county"] = address.County;
            }
            if (!string.IsNullOrWhiteSpace(address.Country))
            {
                parameters["country"] = address.Country;
            }
            if (!string.IsNullOrWhiteSpace(address.Street))
            {
                parameters["street"] = address.Street;
            }

            var requestUrl = $"{this._apiUrl}/rates/{address.Zip}";
            if (parameters.Count > 0)
            {
                if (encodeParameters)
                {
                    requestUrl += $"?{HttpUtility.UrlEncode(string.Join('&', parameters.Select(KeyValuePair => $"{KeyValuePair.Key}={KeyValuePair.Value}")))}";
                }
                else
                {
                    requestUrl += $"?{string.Join('&', parameters.Select(KeyValuePair => $"{KeyValuePair.Key}={KeyValuePair.Value}"))}";
                }
            }

            return requestUrl;
        }
    }
}
