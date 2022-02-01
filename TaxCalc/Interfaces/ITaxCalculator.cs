using System.Threading.Tasks;
using TaxCalc.Model;

namespace TaxCalc.Interfaces
{
    public interface ITaxCalculator
    {
        Task<LocationTaxRates> GetTaxRateForToAddress();
        Task<LocationTaxRates> GetTaxRateForFromAddress();
        Task<double> CalculateTaxesForOrder();
        void SetToAddress(Address address);
        void SetFromAddress(Address address);
        void SetShipping(double shipping);
        void AddOrderProduct(OrderProduct orderProduct);
        void RemoveOrderProduct(int index);
        void ClearOrderProducts();
        void AddNexusAddress(Address address);
        void ClearNexusAddresses();
    }
}
