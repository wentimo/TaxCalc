using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaxCalc.Model;

namespace TaxCalc.Calculators.TaxJar.Model
{
    public class TaxesRequest
    {
        public TaxesRequest(Order order)
        {
            FromCountry = order.FromAddress.Country;
            FromZip = order.FromAddress.Zip;
            FromState = order.FromAddress.State;
            ToCountry = order.ToAddress.Country;
            ToZip = order.ToAddress.Zip;
            ToState = order.ToAddress.State;
            Shipping = order.Shipping;

            // After testing the API it doesn't seem to matter if we don't combine quantities together as long as LineItems is populated
            LineItems = order.OrderProducts.Select(op =>
                new TaxesRequestLineItem()
                {
                    Id = op.Id,
                    Quantity = 1,
                    UnitPrice = op.Price,
                    ProductTaxCode = op.TaxCode,
                }).ToList();

            NexusAddresses = order.NexusAddresses.Select(na =>
                new NexusAddress()
                {
                    Country = na.Country,
                    State = na.State,
                    Zip = na.Zip,
                }).ToList();
        }

        public string ToJson() => JsonSerializer.Serialize(this, new JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = true });

        [JsonPropertyName("to_city")]
        public string ToCity { get; set; }

        [JsonPropertyName("to_state")]
        public string ToState { get; set; }

        [JsonPropertyName("to_zip")]
        public string ToZip { get; set; }

        [JsonPropertyName("to_country")]
        public string ToCountry { get; set; }

        [JsonPropertyName("from_city")]
        public string FromCity { get; set; }

        [JsonPropertyName("from_state")]
        public string FromState { get; set; }

        [JsonPropertyName("from_zip")]
        public string FromZip { get; set; }

        [JsonPropertyName("from_country")]
        public string FromCountry { get; set; }

        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        [JsonPropertyName("shipping")]
        public double Shipping { get; set; }

        [JsonPropertyName("line_items")]
        public List<TaxesRequestLineItem> LineItems { get; set; }

        [JsonPropertyName("nexus_addresses")]
        public List<NexusAddress> NexusAddresses { get; set; }
    }

    public class TaxesRequestLineItem
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public double UnitPrice { get; set; }

        [JsonPropertyName("product_tax_code")]
        public string ProductTaxCode { get; set; }
    }

    public class NexusAddress
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("zip")]
        public string Zip { get; set; }
    }

}
