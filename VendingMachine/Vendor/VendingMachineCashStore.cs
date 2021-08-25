using System.Collections.Generic;
using System.Linq;
using VendingMachine.Coins;

namespace VendingMachine.Vendor
{
    public class VendingMachineCashStore 
    {
        protected readonly List<Coin> _coins;
        internal VendingMachineCashStore(IEnumerable<Coin> startingCoins)
        {
            _coins = new List<Coin>();
            
            AddCoins(startingCoins);
        }

        public IReadOnlyCollection<Coin> Coins => _coins;
        public int AmountInPence => _coins.Sum(x => x.AmountInPence);

        internal int AddCoin(Coin coin)
        {
            _coins.Add(coin);

            return AmountInPence;
        }

        internal int AddCoins(IEnumerable<Coin> coins)
        {
            _coins.AddRange(coins);

            return AmountInPence;
        }

    }
}
