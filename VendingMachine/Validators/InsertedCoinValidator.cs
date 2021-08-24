using System.Collections.Generic;
using System.Linq;
using VendingMachine.Coins;

namespace VendingMachine.Validators
{
    public class InsertedCoinValidator : IValidateInsertedCoin
    {
        private IEnumerable<Coin> InvalidCoins { get; }
        
        public InsertedCoinValidator()
        {
            InvalidCoins = new List<Coin>
            {
                CoinValues.OnePence,
                CoinValues.TwoPence
            };
        }

        public bool IsValid(Coin coin)
        {
            return !InvalidCoins.Contains(coin);
        }
    }
}
