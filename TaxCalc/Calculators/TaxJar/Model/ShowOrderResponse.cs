using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaxCalc.Calculators.TaxJar.Model
{
    public class ShowOrderResponse
    {
        [JsonPropertyName("order")]
        public ShowOrder Order { get; set; }
    }

    public class ShowOrder
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("transaction_reference_id")]
        public object TransactionReferenceId { get; set; }

        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }

        [JsonPropertyName("transaction_date")]
        public DateTime TransactionDate { get; set; }

        [JsonPropertyName("to_zip")]
        public string ToZip { get; set; }

        [JsonPropertyName("to_street")]
        public string ToStreet { get; set; }

        [JsonPropertyName("to_state")]
        public string ToState { get; set; }

        [JsonPropertyName("to_country")]
        public string ToCountry { get; set; }

        [JsonPropertyName("to_city")]
        public string ToCity { get; set; }

        [JsonPropertyName("shipping")]
        public string Shipping { get; set; }

        [JsonPropertyName("sales_tax")]
        public string SalesTax { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("line_items")]
        public List<ShowOrderLineItem> LineItems { get; set; }

        [JsonPropertyName("from_zip")]
        public string FromZip { get; set; }

        [JsonPropertyName("from_street")]
        public string FromStreet { get; set; }

        [JsonPropertyName("from_state")]
        public string FromState { get; set; }

        [JsonPropertyName("from_country")]
        public string FromCountry { get; set; }

        [JsonPropertyName("from_city")]
        public string FromCity { get; set; }

        [JsonPropertyName("exemption_type")]
        public string ExemptionType { get; set; }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }
    }
    public class ShowOrderLineItem
    {
        [JsonPropertyName("unit_price")]
        public string UnitPrice { get; set; }

        [JsonPropertyName("sales_tax")]
        public string SalesTax { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("product_tax_code")]
        public string ProductTaxCode { get; set; }

        [JsonPropertyName("product_identifier")]
        public string ProductIdentifier { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("discount")]
        public string Discount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
