using System.Collections.Generic;

namespace TaxCalc.Model
{
    public class Order
    {
        public Address FromAddress { get; set; }
        public Address ToAddress { get; set; }
        public double Shipping { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public List<Address> NexusAddresses { get; set; } = new List<Address>();
    }
}
