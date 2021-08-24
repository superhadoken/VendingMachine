using Display.Menus;
using Display.Views;
using System.Collections.Generic;
using System.Globalization;
using VendingMachine.Coins;
using VendingMachine.Validators;
using VendingMachine.Vendor;

namespace Display.VendorOrchestrators
{
    public class VendorOrchestration : IOrchestrateVendingMachine
    {
        private readonly Vendor _vendor;
        private readonly DisplayContainer _displayContainer;
        private readonly MenuHandler _menuHandler;

        public VendorOrchestration(IValidateInsertedCoin validateInsertedCoin)
        {
            _menuHandler = new MenuHandler();
            _displayContainer = new DisplayContainer();
            _vendor = new Vendor(validateInsertedCoin);
        }

        public void Run()
        {
            while (true)
            {
                AcceptCoins();

            }

        }

        private void AcceptCoins()
        {
            var sections = new List<DisplaySection>
            {
                new(1, SetVendorStatusMessage()),
                new(2, "Select Coin to insert"),
                new(3, CreateCoinSelect(out var menuOptions))
            };

            _displayContainer.DrawNewScreen(sections);

            var selectedCoin = (Coin)_menuHandler.SelectFromOptions(menuOptions);

            _vendor.AddCoin(selectedCoin);
        }

        private string SetVendorStatusMessage()
        {
            return _vendor.InsertedCoins.AmountInPence < 1
                ? "INSERT COIN"
                : "£" + _vendor.InsertedCoins.AmountInGbp.ToString(CultureInfo.InvariantCulture);
        }

        private static string CreateCoinSelect(out IDictionary<int, object> menuOptions)
        {
            var coinOptions = "";
            var index = 1;
            menuOptions = new Dictionary<int, object>();

            foreach (var coin in CoinValues.Coins)
            {
                coinOptions += $"{index}) {coin.Description} \n";

                menuOptions.Add(index, coin);

                index++;
            }

            return coinOptions;
        }
    }
}
