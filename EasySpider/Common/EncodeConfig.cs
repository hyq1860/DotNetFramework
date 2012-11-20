using System;
using System.Collections.Generic;
using System.Text;

namespace EasySpider
{
    public class CommonConfig
    {
        public static Encoding GetRequestEncode()
        {
            return Encoding.UTF8;
        }

        public static Encoding GetResponseEncode()
        {
            return Encoding.UTF8;
        }

        public static string GetNewLine()
        {
            return "\n";
        }
    }
}
