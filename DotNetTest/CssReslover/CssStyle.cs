using System;
using System.Collections.Generic;

namespace Fantasy.Tools.CssResolve
{
    public struct CssStyle
    {
        public string Name;
        public Int32 LineNumber;
        public IList<PropertyText> Properties;
    }
}
