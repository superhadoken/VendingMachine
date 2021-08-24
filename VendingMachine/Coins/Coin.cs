using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Coins
{
    public static class CoinValues
    {
        public static Coin OnePence => new("1p", 1);
        public static Coin TwoPence => new("2p", 2);
        public static Coin FivePence => new("5p", 5);
        public static Coin TenPence => new("10p", 10);
        public static Coin TwentyPence => new("20p", 20);
        public static Coin FiftyPence => new("50p", 50);
        public static Coin OnePound => new("£1", 100);
        public static Coin TwoPounds => new("£2", 200);

        static CoinValues()
        {
            Coins = new List<Coin>
            {
                OnePence,
                TwoPence,
                FivePence,
                TenPence,
                TwentyPence,
                FiftyPence,
                OnePound,
                TwoPounds
            }.OrderBy(x => x.AmountInPence);
        }

        public static IOrderedEnumerable<Coin> Coins { get; set; }
    }

    public record Coin(string Description, int AmountInPence) { }
}
