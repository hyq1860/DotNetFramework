using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EasySpider
{
    /// <summary>
    /// 一个封装正则相关的字符串处理类
    /// </summary>
    class RegOpration
    {
        /// <summary>
        /// 用指定的正则表达式截取html文本中你想要的内容(全局搜索)
        /// </summary>
        /// <param name="html">html字符串</param>
        /// <param name="reg">正则表达式</param>
        /// <returns>返回匹配的MatchCollection</returns>
        public static MatchCollection SearchByRegex(string html, Regex reg)
        {
            return reg.Matches(html);
        }      
    }
}
