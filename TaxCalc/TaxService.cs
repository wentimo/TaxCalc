using System.Threading.Tasks;
using TaxCalc.Interfaces;
using TaxCalc.Model;

namespace TaxCalc
{
    public class TaxService
    {
        private readonly ITaxCalculator _taxCalculator;

        public TaxService(ITaxCalculator taxCalculator)
        {
            this._taxCalculator = taxCalculator;
        }

        public async Task<LocationTaxRates> GetTaxRateForToAddress() =>
            await this._taxCalculator.GetTaxRateForToAddress();

        public async Task<LocationTaxRates> GetTaxRateForFromAddress() =>
            await this._taxCalculator.GetTaxRateForFromAddress();

        public async Task<double> CalculateTaxesForOrder() =>
            await this._taxCalculator.CalculateTaxesForOrder();

        public void SetToAddress(Address address) =>
            this._taxCalculator.SetToAddress(address);

        public void SetToAddress(string city = null, string street = null, string zip = null, string state = null, string country = null, string county = null) =>
            this._taxCalculator.SetToAddress(new Address(zip, street, state, county, country, city));

        public void SetFromAddress(Address address) =>
            this._taxCalculator.SetFromAddress(address);

        public void SetFromAddress(string city = null, string street = null, string zip = null, string state = null, string country = null, string county = null) =>
            this._taxCalculator.SetFromAddress(new Address(zip, street, state, county, country, city));

        public void SetShipping(double shipping) =>
            this._taxCalculator.SetShipping(shipping);

        public void AddOrderProduct(OrderProduct orderProduct) =>
            this._taxCalculator.AddOrderProduct(orderProduct);

        public void AddOrderProduct(double price, string taxCode = null, int? id = null) =>
            this._taxCalculator.AddOrderProduct(new OrderProduct(price, taxCode, id));

        public void RemoveOrderProduct(int index) =>
            this._taxCalculator.RemoveOrderProduct(index);

        public void ClearOrderProducts() =>
            this._taxCalculator.ClearOrderProducts();

        public void AddNexusAddress(Address address) =>
            this._taxCalculator.AddNexusAddress(address);

        public void AddNexusAddress(string city = null, string street = null, string zip = null, string state = null, string country = null, string county = null) =>
            this._taxCalculator.AddNexusAddress(new Address(zip, street, state, county, country, city));

        public void ClearNexusAddresses() =>
            this._taxCalculator.ClearNexusAddresses();
    }
}
