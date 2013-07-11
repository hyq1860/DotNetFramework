using System;
using System.Collections.Generic;
using System.Text;

namespace Fantasy.Tools.CssResolve
{
    public static class CssExtension
    {
        /// <summary>
        /// Remove space and line-break chars at the end
        /// </summary>
        public static String Trim(this IList<char> chars)
        {
            for (int i = chars.Count - 1; i >= 0; i--)
            {
                var c = chars[i];
                if (Char.IsWhiteSpace(c) || Char.IsSeparator(c))
                    chars.RemoveAt(i);
                else break;
            }
            return chars.GetString();
        }

        public static String GetString(this IList<char> chars)
        {
            char[] array = new char[chars.Count];
            chars.CopyTo(array, 0);
            chars.Clear();
            return new String(array);
        }

        internal static void Print(this IList<CssStyle> styles)
        {
            foreach (var item in styles)
            {
                Console.WriteLine("line {0}:{2}{1} :", item.LineNumber, item.Name, Console.Out.NewLine);
                var style = item.Properties;
                foreach (var prop in style)
                {
                    Console.WriteLine("\t" + prop);
                }
            }
        }
    }   
}
