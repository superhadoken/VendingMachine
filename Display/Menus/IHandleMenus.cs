using System.Collections.Generic;

namespace Display.Menus
{
    public interface IHandleMenus
    {
        object SelectFromOptions(IDictionary<int, object> options);
    }
}
