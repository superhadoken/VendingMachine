using System.Collections.Generic;
using VendingMachine.Coins;
using VendingMachine.Products;
using VendingMachine.Validators;

namespace VendingMachine.Vendor
{
    public class Vendor
    {
        private readonly IValidateInsertedCoin _insertedCoinValidator;
        private readonly List<Coin> _coinReturnBasket;
        private readonly List<Coin> _initialVendingMachineCoins = new()
        {
            CoinValues.TenPence,
            CoinValues.FiftyPence,
            CoinValues.OnePound,
            CoinValues.TwentyPence
        };

        public Vendor(IValidateInsertedCoin insertedCoinValidator)
        {
            _insertedCoinValidator = insertedCoinValidator;

            _coinReturnBasket = new List<Coin>();
            CustomerCreditPence = 0;
            VendingMachineCashStore = new VendingMachineCashStore(_initialVendingMachineCoins);

            // Product Stock can come from config file etc, wouldn't normally be hardcoded here
            ProductStocks = new Dictionary<Product, int>
            {
                {ProductsList.Cola, 4},
                {ProductsList.Crisps, 2},
                {ProductsList.Chocolate, 1}
            };
        }

        public IList<Coin> CoinReturnBasket => _coinReturnBasket;
        public int CustomerCreditPence { get; private set; }
        public double CustomerCreditGbp => CustomerCreditPence / 100.0;
        public VendingMachineCashStore VendingMachineCashStore { get; private set; }
        public IDictionary<Product, int> ProductStocks { get; private set; }

        //public IEnumerable<Coin> ReturnCoins()
        //{
        //    _coinReturnBasket.AddRange(InsertedCoins.ReturnCoins());

        //    return CoinReturnBasket;
        //}

        public int? AddCoin(Coin coin)
        {
            if (_insertedCoinValidator.IsValid(coin))
            {
                CustomerCreditPence += coin.AmountInPence;

                VendingMachineCashStore.AddCoin(coin);

                return CustomerCreditPence;
            }

            _coinReturnBasket.Add(coin);

            return null;
        }

        public bool PurchaseProduct(Product selectedProduct)
        {
            if (!CanPurchaseProduct(selectedProduct))
                return false;

            ProductStocks[selectedProduct] -= 1;

            CustomerCreditPence -= selectedProduct.CostInPence;

            return true;
        }

        private bool CanPurchaseProduct(Product selectedProduct)
        {
            var productExistsInStock = ProductStocks.TryGetValue(selectedProduct, out var productStockQty);

            //todo: need to actually check the coins inserted and how much cash is in the machine to make sure we can give change too - implement later
            return CustomerCreditPence >= selectedProduct.CostInPence
                   && productExistsInStock
                   && productStockQty > 0;
        }

    }
}
