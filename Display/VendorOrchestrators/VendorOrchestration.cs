using Display.Menus;
using Display.Views;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Coins;
using VendingMachine.Products;
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
                CreateMainMenu();
            }
        }

        private void CreateMainMenu()
        {
            var sections = new List<DisplaySection>
            {
                new(1, SetVendorStatusMessage()),
                new(2, "Select Vending Machine Action"),
                new(3, CreateMainMenuSelect(out var menuOptions))
            };

            _displayContainer.DrawNewScreen(sections);

            var (selectedMenuOption, _) = _menuHandler.SelectFromOptions(menuOptions);
            
            switch (selectedMenuOption)
            {
                case 1: AcceptCoins();
                    break;
                case 2: SelectProduct();
                    break;
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

            var (_, selectedCoin) = _menuHandler.SelectFromOptions(menuOptions);

            _vendor.AddCoin((Coin)selectedCoin);
        }

        private void SelectProduct()
        {
            var sections = new List<DisplaySection>
            {
                new(1, SetVendorStatusMessage()),
                new(2, "Select Product to Purchase"),
                new(3, CreateProductSelect(out var menuOptions))
            };

            _displayContainer.DrawNewScreen(sections);

            var (_, selectedProduct) = _menuHandler.SelectFromOptions(menuOptions);

            var purchaseSucceeded = _vendor.PurchaseProduct((Product) selectedProduct);

            SimpleDisplayMessage(purchaseSucceeded ? "THANK YOU" : "PRICE"); //todo: this assumes you don't have enough money if you fail - introduce check
        }

        private string SetVendorStatusMessage()
        {
            if (_vendor.CustomerCreditPence < 1)
            {
                return _vendor.VendingMachineCashStore.Coins.Any() 
                    ? "INSERT COIN" 
                    : "EXACT CHANGE ONLY";
            }
            
            return _vendor.CustomerCreditGbp.ToString("C");
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

        private static string CreateProductSelect(out IDictionary<int, object> menuOptions)
        {
            var productOptions = "";
            var index = 1;
            menuOptions = new Dictionary<int, object>();

            foreach (var product in ProductsList.Products)
            {
                productOptions += $"{index}) {product.Description} - {product.CostInGbp:C} \n";

                menuOptions.Add(index, product);

                index++;
            }

            return productOptions;
        }

        //todo selectors like this and above can be pushed into service/separated out 
        private static string CreateMainMenuSelect(out IDictionary<int, object> menuOptions)
        {
            const string addCoins = "Add Coin(s)";
            const string selectProduct = "Select Product";

            menuOptions = new Dictionary<int, object>
            {
                {1, addCoins },
                {2, selectProduct}
            };

            return $"1) {addCoins}\n2) {selectProduct}";
        }

        private void SimpleDisplayMessage(string message)
        {
            var sections = new List<DisplaySection>
            {
                new(1, message)
            };

            _displayContainer.DrawNewScreen(sections);
        }
    }
}
