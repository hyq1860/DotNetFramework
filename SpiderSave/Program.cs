using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderSave
{
    using EasySpider;

    class Program
    {
        static void Main(string[] args)
        {
            using (CHttpWebResponse cResponse = Spider.Get(Spider.CreateRequest("http://www.360buy.com/product/598982.html")))
            {
                WebPage page = cResponse.GetWebPage();
                //第一个参数指定保存的文件名；第二个参数表示不过滤js；第三个参数指定保存文件夹（还可以为css、js等指定不同的文件夹，只要在同一目录下就行）
                page.SaveHtmlAndResource(@"598982.html", false, new DirConfig(@"z:\598982"));
            }
        }
    }
}
