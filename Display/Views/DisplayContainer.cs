using System;
using System.Collections.Generic;
using System.Linq;

namespace Display.Views
{
    public class DisplayContainer
    {
        private List<DisplaySection> _displaySections;

        public DisplayContainer()
        {
            _displaySections = new List<DisplaySection>();
        }

        public IReadOnlyCollection<DisplaySection> DisplaySections => _displaySections;

        public void DrawCurrentScreen()
        {
            var orderedDisplay = DisplaySections.OrderBy(x => x.Priority);

            foreach (var displaySection in orderedDisplay)
            {
                Console.WriteLine(displaySection.Content);
            }
        }

        public void DrawNewScreen(List<DisplaySection> displaySections)
        {
            NewScreen(displaySections);

            var orderedDisplay = DisplaySections.OrderBy(x => x.Priority);

            foreach (var displaySection in orderedDisplay)
            {
                Console.WriteLine(displaySection.Content);
            }
        }

        public void NewScreen(List<DisplaySection> displaySections)
        {
            _displaySections = displaySections;
        }

        public void Clear()
        {
            _displaySections.Clear();
        }
    }
}
