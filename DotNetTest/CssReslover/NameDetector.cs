using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fantasy.Tools.Text;

namespace Fantasy.Tools.CssResolve
{
    class NameDetector
    {
        public bool Enabled = true;
        public string Key;
        public int Index;

        public NameDetector(string key)
        {
            if (String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            Index = 0;
            Key = key;
        }

        public MatchResult Match(char c)
        {
            if (c == Key[Index])
            {
                Index++;
                if (Index >= Key.Length)
                {
                    Index = 0;
                    return MatchResult.Success;
                }
                return MatchResult.Continue;
            }
            else if (c == Key[0])
            {
                Index = 1;
                return MatchResult.Continue;
            }
            Index = 0;
            return MatchResult.NotMatch;
        }

        public Char this[int index]
        {
            get { return Key[index]; }
        }
    }
}
