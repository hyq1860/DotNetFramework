// -----------------------------------------------------------------------
// <copyright file="AutoUpdateFileInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 自动更新文件信息
    /// </summary>
    public class AutoUpdateFileInfo
    {
        public string Path { get; set; }

        public string Url { get; set; }

        public string LastVer { get; set; }

        public long Size { get; set; }

        public bool NeedRestart { get; set; }
    }
}
