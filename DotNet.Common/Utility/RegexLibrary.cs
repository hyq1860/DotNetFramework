// -----------------------------------------------------------------------
// <copyright file="RegexLibrary.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace DotNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 常见的正则表达式库
    /// </summary>
    public class RegexLibrary
    {
        public static RegexOptions RegOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;

        #region 常用正则表达式
        /// <summary>
        /// 判读一个字符串是否url的正则，其中Groups如下：protocol是协议名，domain是url的域名，port是域名对应的端口，path是url的在服务器上代表的绝对路径（例如/aa/bb,或者/aa/bb.cc/dd.html等等），query是GET传值部分（不包括问好），hash是#后面的部分
        /// </summary>
        public static Regex RegUrl = new Regex(@"^(?<protocol>https?)://(?<domain>(?:[0-9a-z-_]+(?<!-)\.){0,}[0-9a-z-_]+(?<!-)\.[0-9a-z-_]+|localhost)(?:\:(?<port>\d+))?(?<!-)(?<path>(?:(?<!/)/[0-9a-z-_.]+/?){0,})(?:\?(?<query>[^#]+))?(?:#(?<hash>[^\r\n]*))?$", RegOptions);
        /*^https?://(?<prefixdomain>(?:[0-9a-z-_]+(?<!-)\.){0,})(?<topdomain>[0-9a-z-_]+(?<!-)\.[0-9a-z-_]+)(?:\:(?<port>\d+))?(?<!-)/?(?<path>(?:(?<!/)/[0-9a-z-_.]+/?){0,})(?:\?(?<query>[^#]+))?(?:#(?<fragment>[^\r\n]*))?$*/

        /// <summary>
        /// 验证form中action的正则，其中Groups["quote"]为单引号，双引号，或者空字符串，Groups["action"]为action的值
        /// </summary>66
        public static Regex RegFormAction = new Regex(@"<form[\s\S]+?action=(?<quote>['""]?)(?<action>[^'""\s><]+)\1[\s\S]*?>", RegOptions);

        /// <summary>
        /// 验证href的正则，其中Groups["quote"]为单引号，双引号，或者空字符串，Groups["href"]为href的值
        /// </summary>
        public static Regex RegHref = new Regex(@"href=(?<quote>['""]?)(?<href>[^'""\s><]+)\k<quote>", RegOptions);

        /// <summary>
        /// 验证src的正则，其中Groups["quote"]为单引号，双引号，或者空字符串，Groups["src"]为src的值
        /// </summary>
        public static Regex RegSrc = new Regex(@"src=(?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>", RegOptions);

        /// <summary>
        /// 从一个域名中获取一级域名
        /// </summary>
        public static Regex RegLevelDomain = new Regex(@"[^\.]+\.[^\.]+$", RegOptions);

        /// <summary>
        /// 获取HTML中Meta标签中的Content-Type值
        /// </summary>
        public static Regex RegMetaContentType = new Regex(@"<\s*meta[^<>]+content=['""]?text/html;charset=([\w-]+)['""]?[^<>]*>", RegOptions);

        /// <summary>
        /// 一个匹配Script标签的正则,Groups[1]是两个script标签之间的内容，它可以匹配如下内容：
        /*
        <script language=""javascript"" type='text/javascript' src='../../1.js'>   
                  function getColor() {
                       alert(123)
                   }          
                  //因为<script>左边有引号，</script>右边有引号，所以匹配
                  document.write('<script></script>');   
                 </script>
         * */
        /// </summary>
        public static Regex RegScript = new Regex(@"<script[^>]*>(((?!(?<!['""])<\/?script>(?!['""]))[\s\S])*)</script>", RegOptions);

        /// <summary>
        /// 验证css引入，如果匹配成功，则Groups["src"]是css文件href的值
        /// </summary>
        public static Regex RegCssLink = new Regex(@"<link[^>]*(?:(?:href=(?<quote1>['""]?)(?<src>[^'""\s><]+)\k<quote1>[^>]*rel=(?<quote2>['""]?)stylesheet\k<quote2>)|(?:rel=(?<quote3>['""]?)stylesheet\k<quote3>[^>]*href=(?<quote4>['""]?)(?<src>[^'""\s><]+)\k<quote4>))[^>]*>", RegOptions);

        /// <summary>
        /// 验证script脚本引入，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是Js文件src的值
        /// </summary>
        public static Regex RegScriptLink = new Regex(@"<script[^>]*src=(?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>[^>]*></script>", RegOptions);

        /// <summary>
        /// 验证Img图片引入，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是img标签src的值
        /// </summary>
        public static Regex RegImg = new Regex(@"<img[^>]*src=(?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>[^>]*>", RegOptions);

        /// <summary>
        /// 验证Falsh引入，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是flash的src的值
        /// </summary>
        public static Regex RegFlash = new Regex(@"(?<quote>['""]?)(?<src>[^'""\s><]+\.swf)\k<quote>", RegOptions);

        /// <summary>
        /// 验证css文件中引用的图片，如果匹配成功，其中Groups["quote"]为单引号，双引号，或者空字符串，则Groups["src"]是背景图片的url
        /// </summary>
        public static Regex RegCssImgUrl = new Regex(@"url\((?<quote>['""]?)(?<src>[^'""\s><]+)\k<quote>\)", RegOptions);

        /// <summary>
        /// 验证css文件中指定的编码类型，如果匹配成功，则Groups[2]是css文件的编码类型
        /// </summary>
        public static Regex RegCssContentType = new Regex(@"@charset\s*([""']?)([^'"";\s]+)\1", RegOptions);
        #endregion

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
