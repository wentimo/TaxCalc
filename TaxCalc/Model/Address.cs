namespace TaxCalc.Model
{
    public class Address
    {
        public Address(string zip = null, string street = null, string state = null, string county = null, string country = null, string city = null)
        {
            Zip = zip;
            Street = street;
            State = state;
            County = county;
            Country = country;
            City = city;
        }

        public string Zip { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}