using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fantasy.Tools.CssResolve
{
    public struct PropertyText
    {
        public string Name;
        public string Text;
        public Int32 StartPosition;

        public override string ToString()
        {
            return string.Concat(Name, ", ", Text, ", ", StartPosition);
        }
    }
}
