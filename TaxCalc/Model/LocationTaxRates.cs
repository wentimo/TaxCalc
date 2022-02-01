using TaxCalc.Calculators.TaxJar.Model;

namespace TaxCalc.Model
{
    public class LocationTaxRates
    {
        public LocationTaxRates(RatesResponse taxJarLocationRate)
        {
            CityRate = string.IsNullOrEmpty(taxJarLocationRate.CityRate) ? 0 : double.Parse(taxJarLocationRate.CityRate);
            CountryRate = string.IsNullOrEmpty(taxJarLocationRate.CountryRate) ? 0 : double.Parse(taxJarLocationRate.CountryRate);
            CountyRate = string.IsNullOrEmpty(taxJarLocationRate.CountyRate) ? 0 : double.Parse(taxJarLocationRate.CountyRate);
            StateRate = string.IsNullOrEmpty(taxJarLocationRate.StateRate) ? 0 : double.Parse(taxJarLocationRate.StateRate);
            CombinedDistrictRate = string.IsNullOrEmpty(taxJarLocationRate.CombinedDistrictRate) ? 0 : double.Parse(taxJarLocationRate.CombinedDistrictRate);
            CombinedRate = string.IsNullOrEmpty(taxJarLocationRate.CombinedRate) ?
                CityRate + CountyRate + CountryRate + StateRate + CombinedDistrictRate : 
                double.Parse(taxJarLocationRate.CombinedRate);

            ParkingRate = string.IsNullOrEmpty(taxJarLocationRate.ParkingRate) ? 0 : double.Parse(taxJarLocationRate.ParkingRate);
            ReducedRate = string.IsNullOrEmpty(taxJarLocationRate.ReducedRate) ? 0 : double.Parse(taxJarLocationRate.ReducedRate);
            StandardRate = string.IsNullOrEmpty(taxJarLocationRate.StandardRate) ? 0 : double.Parse(taxJarLocationRate.StandardRate);
            SuperReducedRate = string.IsNullOrEmpty(taxJarLocationRate.SuperReducedRate) ? 0 : double.Parse(taxJarLocationRate.SuperReducedRate);
        }

        public double CityRate { get; set; }
        public double CountryRate { get; set; }
        public double CountyRate { get; set; }
        public double StateRate { get; set; }
        public double CombinedDistrictRate { get; set; }
        public double CombinedRate { get; set; }
        public double ParkingRate { get; set; }
        public double ReducedRate { get; set; }
        public double StandardRate { get; set; }
        public double SuperReducedRate { get; set; }
    }
}
