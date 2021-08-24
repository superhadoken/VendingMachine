using System.Collections.Generic;

namespace Display.Menus
{
    public interface IHandleMenus
    {
        KeyValuePair<int, object> SelectFromOptions(IDictionary<int, object> options);
    }
}
