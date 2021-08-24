using System;
using System.Collections.Generic;

namespace Display.Menus
{
    public class MenuHandler : IHandleMenus
    {
        public MenuHandler()
        {
        }

        public IDictionary<int, object> Options { get; private set; }

        public KeyValuePair<int, object> SelectFromOptions(IDictionary<int, object> options)
        {
            Options = options;

            //todo add validation to this catch if not an int and display error message
            var userSelectedOption = Convert.ToInt32(Console.ReadLine());

            //todo - validate that selection was within range
            Options.TryGetValue(userSelectedOption, out var selectedValue);

            return new KeyValuePair<int, object>(userSelectedOption, selectedValue);
        }
    }
}
