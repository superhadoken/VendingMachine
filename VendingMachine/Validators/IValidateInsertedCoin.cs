using VendingMachine.Coins;

namespace VendingMachine.Validators
{
    public interface IValidateInsertedCoin
    {
        bool IsValid(Coin coin);
    }
}
