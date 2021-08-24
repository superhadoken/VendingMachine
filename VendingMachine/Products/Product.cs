namespace VendingMachine.Products
{
    public record Product
    {
        public string Description { get; init; }
        public decimal Cost { get; init; }
    }
}
