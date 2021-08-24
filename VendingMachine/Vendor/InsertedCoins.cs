using VendingMachine.Coins;

namespace VendingMachine.Vendor
{
    public class InsertedCoins
    {
        internal InsertedCoins(int amountInPence)
        {
            AmountInPence = amountInPence;
        }

        public int AmountInPence { get; private set; }
        public double AmountInGbp => AmountInPence / 100.0;

        internal int ReturnCoins()
        {
            var originalAmount = AmountInPence;
            
            AmountInPence = 0;

            return originalAmount;
        }

        internal int AddCoin(Coin coin)
        {
            AmountInPence += coin.AmountInPence;

            return AmountInPence;
        }
    }
}
