using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TaxCalc;
using TaxCalc.Calculators.TaxJar;
using TaxCalc.Exceptions;

namespace TaxCalcTests.Calculators.TaxJar
{
    public class TaxJarTests
    {
        private readonly string _apiKey = "5da2f821eee4035db4771edab942a4cc";

        TaxService _service;
        HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            this._client = new HttpClient();
            this._service = new TaxService(new TaxJarCalculator(this._apiKey, this._client));
        }

        [SetUp]
        public void ResetOrder()
        {
            this._service.SetToAddress(null);
            this._service.SetFromAddress(null);
            this._service.SetShipping(0);
            this._service.ClearOrderProducts();
            this._service.ClearNexusAddresses();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            this._client.Dispose();
        }

        [Test]
        public void RatesForLocation()
        {
            this._service.SetToAddress(zip: "90404");
            Assert.DoesNotThrowAsync(async () => await this._service.GetTaxRateForToAddress());

            this._service.SetToAddress(zip: null);
            Assert.ThrowsAsync<ArgumentException>(async () => await this._service.GetTaxRateForToAddress());
        }

        [Test]
        public async Task RatesForLocationUS()
        {
            this._service.SetToAddress(zip: "90404");
            var rate = await this._service.GetTaxRateForToAddress();

            Assert.AreEqual(rate.CombinedDistrictRate, 0.03);
            Assert.AreEqual(rate.CountyRate, 0.01);
            Assert.AreEqual(rate.StateRate, 0.0625);
            Assert.AreEqual(rate.CombinedRate, 0.1025);
        }
        [Test]
        public async Task RatesForLocationUSWARooftop()
        {
            this._service.SetToAddress(zip: "98109", street:"400 Broad St", city: "Seattle");
            var rate = await this._service.GetTaxRateForToAddress();

            Assert.AreEqual(rate.CombinedDistrictRate, 0.023);
            Assert.AreEqual(rate.CityRate, 0.0115);
            Assert.AreEqual(rate.CountyRate, 0.003);
            Assert.AreEqual(rate.StateRate, 0.065);
            Assert.AreEqual(rate.CombinedRate, 0.1025, 0.0001);
        }

        [Test]
        public async Task RatesForLocationCA()
        {
            this._service.SetToAddress(zip: "V5K0A1", city: "Vancouver", country: "CA");
            var rate = await this._service.GetTaxRateForToAddress();
            Assert.AreEqual(rate.CombinedRate, 0.12);
        }

        [Test]
        public async Task RatesForLocationEU()
        {
            this._service.SetToAddress(zip: "00150", city: "Helsinki", country: "FI");
            var rate = await this._service.GetTaxRateForToAddress();
            Assert.AreEqual(rate.ParkingRate, 0.0);
            Assert.AreEqual(rate.ReducedRate, 0.14);
            Assert.AreEqual(rate.StandardRate, 0.24);
            Assert.AreEqual(rate.SuperReducedRate, 0.1);
        }

        [Test]
        public async Task RatesForLocationAU()
        {
            this._service.SetToAddress(zip: "2000", city: "Sydney", country: "AU");
            var rate = await this._service.GetTaxRateForToAddress();
            Assert.AreEqual(rate.CombinedRate, 0.1);
        }

        [Test]
        public async Task TaxForOrderUS()
        {
            this._service.SetToAddress(state: "NJ", zip: "07746", country: "US");
            this._service.SetFromAddress(state: "NJ", zip: "07001", country: "US");
            this._service.SetShipping(1.5);

            this._service.AddOrderProduct(price: 15.0, taxCode: "31000");
            Assert.AreEqual(await this._service.CalculateTaxesForOrder(), 1.09);

            this._service.AddOrderProduct(price: 15.0, taxCode: "31000");
            Assert.AreEqual(await this._service.CalculateTaxesForOrder(), 2.09);

            this._service.SetShipping(15);
            Assert.AreEqual(await this._service.CalculateTaxesForOrder(), 2.98);
        }

        [Test]
        public async Task TaxForOrderUSNYProductExemption()
        {
            this._service.SetToAddress(city: "Mahopac", state: "NY", zip: "10541", country: "US");
            this._service.SetFromAddress(city: "Delmar", state: "NY", zip: "12054", country: "US");
            this._service.SetShipping(7.99);
            this._service.AddOrderProduct(price: 19.99, taxCode: "20010");
            this._service.AddOrderProduct(price: 9.95, taxCode: "20010");

            var taxes = await this._service.CalculateTaxesForOrder();
            Assert.AreEqual(taxes, 1.98);
        }

        [Test]
        public async Task TaxForOrderUSNCAProductExemption()
        {
            this._service.SetFromAddress(city: "San Francisco", street: "600 Montgomery St", state: "CA", zip: "94111", country: "US");
            this._service.SetToAddress(city: "Campbell", street: "33 N. First Street", state: "CA", zip: "95008", country: "US");
            this._service.SetShipping(10);
            this._service.AddOrderProduct(price: 16.95, taxCode: "40030", id: 3);

            Assert.AreEqual(await this._service.CalculateTaxesForOrder(), 0.0);
        }

        [Test]
        public async Task TaxForOrderCA()
        {
            this._service.SetFromAddress(state: "BC", zip: "V6G 3E", country: "CA");
            this._service.SetToAddress(state: "ON", zip: "M5V 2T6", country: "CA");
            this._service.SetShipping(10);

            this._service.AddOrderProduct(price: 16.95);
            Assert.AreEqual(await this._service.CalculateTaxesForOrder(), 3.5);
        }

        [Test]
        public void TaxForOrderNoNexus()
        {
            this._service.SetFromAddress(city: "San Francisco", street: "600 Montgomery St", state: "CA", zip: "94111", country: "US");
            this._service.SetToAddress(city: "Orlando", street: "200 S Orange Ave", state: "FL", zip: "32801", country: "US");
            this._service.SetShipping(10);

            this._service.AddOrderProduct(price: 16.95, taxCode: "40030", id: 3);
            Assert.ThrowsAsync<InvalidOrderException>(async () => await this._service.CalculateTaxesForOrder());
        }

        [Test]
        public async Task TaxForOrderNexusAddresses()
        {
            this._service.SetFromAddress(city: "Orlando", state: "FL", zip: "32801", country: "US");
            this._service.AddNexusAddress(country: "US", state: "FL", zip: " 32801");

            this._service.SetToAddress(city: "Kansas City", state: "MO", zip: "64155", country: "US");
            this._service.AddNexusAddress(country: "US", state: "MO", zip: "63101");

            this._service.SetShipping(7.99);

            this._service.AddOrderProduct(price: 19.99);
            this._service.AddOrderProduct(price: 9.95);
            Assert.AreEqual(await this._service.CalculateTaxesForOrder(), 2.9);
        }
    }
}
