using System.Collections.Generic;
using VendingMachine.Coins;
using VendingMachine.Products;
using VendingMachine.Validators;

namespace VendingMachine.Vendor
{
    public class Vendor
    {
        private readonly IValidateInsertedCoin _insertedCoinValidator;
        private const int PettyCashInit = 300;

        public Vendor(IValidateInsertedCoin insertedCoinValidator)
        {
            _insertedCoinValidator = insertedCoinValidator;
            
            InsertedCoins = new InsertedCoins(0);
            MachinePettyCash = new MachinePettyCash(PettyCashInit);
            ProductStocks = new Dictionary<Product, int>();
            ReturnedCash = 0;
        }

        public int ReturnedCash { get; private set; }
        public InsertedCoins InsertedCoins { get; private set; }
        public MachinePettyCash MachinePettyCash { get; private set; }
        public IDictionary<Product, int> ProductStocks { get; private set; }

        public int ReturnCoins()
        {
            ReturnedCash += InsertedCoins.ReturnCoins();

            return ReturnedCash;
        }

        public int? AddCoin(Coin coin)
        {
            if(_insertedCoinValidator.IsValid(coin))
                return InsertedCoins.AddCoin(coin);

            return null;
        }
    }
}
