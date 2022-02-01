namespace TaxCalc.Model
{
    public class OrderProduct
    {
        public OrderProduct(double price, string taxCode = null, int? id = null)
        {
            Id = Id;
            Price = price;
            TaxCode = taxCode;
        }

        public int? Id { get; set; }
        public double Price { get; set; }
        public string TaxCode { get; set; }
    }
}
