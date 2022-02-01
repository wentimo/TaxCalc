using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaxCalc.Calculators.TaxJar.Model
{
    public class TaxesResponse
    {
        [JsonPropertyName("tax")]
        public Tax Tax { get; set; }
    }

    public class TaxesResponseLineItem
    {
        [JsonPropertyName("city_amount")]
        public double CityAmount { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public double CityTaxRate { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public double CityTaxableAmount { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonPropertyName("county_amount")]
        public double CountyAmount { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public double CountyTaxableAmount { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("special_district_amount")]
        public double SpecialDistrictAmount { get; set; }

        [JsonPropertyName("special_district_taxable_amount")]
        public double SpecialDistrictTaxableAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonPropertyName("state_amount")]
        public double StateAmount { get; set; }

        [JsonPropertyName("state_sales_tax_rate")]
        public double StateSalesTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public double StateTaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }
    }

    public class Shipping
    {
        [JsonPropertyName("city_amount")]
        public double CityAmount { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public double CityTaxRate { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public double CityTaxableAmount { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonPropertyName("county_amount")]
        public double CountyAmount { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public double CountyTaxableAmount { get; set; }

        [JsonPropertyName("special_district_amount")]
        public double SpecialDistrictAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonPropertyName("special_taxable_amount")]
        public double SpecialTaxableAmount { get; set; }

        [JsonPropertyName("state_amount")]
        public double StateAmount { get; set; }

        [JsonPropertyName("state_sales_tax_rate")]
        public double StateSalesTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public double StateTaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }
    }

    public class Breakdown
    {
        [JsonPropertyName("city_tax_collectable")]
        public double CityTaxCollectable { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public double CityTaxRate { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public double CityTaxableAmount { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonPropertyName("county_tax_collectable")]
        public double CountyTaxCollectable { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public double CountyTaxableAmount { get; set; }

        [JsonPropertyName("line_items")]
        public List<TaxesResponseLineItem> LineItems { get; set; }

        [JsonPropertyName("shipping")]
        public Shipping Shipping { get; set; }

        [JsonPropertyName("special_district_tax_collectable")]
        public double SpecialDistrictTaxCollectable { get; set; }

        [JsonPropertyName("special_district_taxable_amount")]
        public double SpecialDistrictTaxableAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonPropertyName("state_tax_collectable")]
        public double StateTaxCollectable { get; set; }

        [JsonPropertyName("state_tax_rate")]
        public double StateTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public double StateTaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }
    }

    public class Jurisdictions
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }
    }

    public class Tax
    {
        [JsonPropertyName("amount_to_collect")]
        public double AmountToCollect { get; set; }

        [JsonPropertyName("breakdown")]
        public Breakdown Breakdown { get; set; }

        [JsonPropertyName("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonPropertyName("has_nexus")]
        public bool HasNexus { get; set; }

        [JsonPropertyName("jurisdictions")]
        public Jurisdictions Jurisdictions { get; set; }

        [JsonPropertyName("order_total_amount")]
        public double OrderTotalAmount { get; set; }

        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("shipping")]
        public double Shipping { get; set; }

        [JsonPropertyName("tax_source")]
        public string TaxSource { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }
    }
}
