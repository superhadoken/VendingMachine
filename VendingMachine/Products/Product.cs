using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Products
{
    public record Product
    {
        public string Description { get; init; }
        public int CostInPence { get; init; }
        public double CostInGbp => CostInPence / 100.0;
    }

    // ideally wouldn't hard code like this, come from config file etc
    public static class ProductsList
    {
        public static Product Cola = new() {Description = "Cola", CostInPence = 100};
        public static Product Crisps = new() {Description = "Crisps", CostInPence = 50};
        public static Product Chocolate = new() {Description = "Chocolate", CostInPence = 65};

        static ProductsList()
        {
            Products = new List<Product>
            {
                Cola,
                Crisps,
                Chocolate
            }.OrderBy(x => x.Description); 
        }

        public static IOrderedEnumerable<Product> Products { get; set; } // orderedEnumerable as we always want the ordering to be guaranteed 
    }
}
