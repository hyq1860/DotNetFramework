using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNet.EnterpriseWebSite
{
    using DotNet.Base.Contract;
    using DotNet.Base.Service;

    public static class SiteConfigManager
    {
        static SiteConfigManager()
        {
            if (Options == null)
            {
                Options = new Dictionary<string, string>();
            }
            else
            {
                Options.Clear();
            }
            ISiteOptionService siteOptionService = new SiteOptionService();
            var data = siteOptionService.GetSiteOption();
            foreach (var siteOptionInfo in data)
            {
                if(Options.ContainsKey(siteOptionInfo.OptionKey))
                {
                    Options[siteOptionInfo.OptionKey] = siteOptionInfo.OptionValue;
                }
                else
                {
                    Options.Add(siteOptionInfo.OptionKey,siteOptionInfo.OptionValue);
                }
            }
        }
        /// <summary>
        /// 在 Html 中直接使用
        /// </summary>
        /// <param name="htmlhelper"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Option(this System.Web.UI.Page page, string key)
        {
            if (IsModify)
            {
                if(Options==null)
                {
                    Options=new Dictionary<string, string>();
                }
                else
                {
                    Options.Clear();
                }
                ISiteOptionService siteOptionService = new SiteOptionService();
                var data = siteOptionService.GetSiteOption();
                foreach (var siteOptionInfo in data)
                {
                    if(Options.ContainsKey(siteOptionInfo.OptionKey))
                    {
                        Options[siteOptionInfo.OptionKey] = siteOptionInfo.OptionValue;
                    }
                    else
                    {
                        Options.Add(siteOptionInfo.OptionKey,siteOptionInfo.OptionValue);
                    }
                }
                IsModify = false;
            }
            return Options[key];
        }



        private static Dictionary<string, string> Options { get; set; } 

        public static bool IsModify { get; set; }
    }
}