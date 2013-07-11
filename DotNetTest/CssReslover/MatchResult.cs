using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fantasy.Tools.Text
{
    public enum MatchResult { NotMatch, Success, Continue }

    public delegate StateReader StateReader(char c);
}
