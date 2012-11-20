using System;
using System.Collections.Generic;
using System.Text;

namespace EasySpider
{
    public class Mime
    {
        /// <summary>
        /// 所有Mime类型，key是文件后缀名（不带点），value是mime类型
        /// </summary>
        static Dictionary<string, string> Mimes;

        /// <summary>
        /// 所有Mime类型，key是mime类型，value是文件后缀名（不带点）
        /// </summary>
        static Dictionary<string, string> MimesReverse;

        /// <summary>
        /// 默认mime类型
        /// </summary>
        static string DefaultMime = "application/octet-stream";

        /// <summary>
        /// 静态构造，在此构造里初始化静态成员
        /// </summary>
        static Mime()
        {
            Mimes = new Dictionary<string, string>(100);
            #region 初始化mime成员
            Mimes.Add("323", "text/h323");
            Mimes.Add("acx", "application/internet-property-stream");
            Mimes.Add("ai", "application/postscript");
            Mimes.Add("aif", "audio/x-aiff");
            Mimes.Add("aifc", "audio/x-aiff");
            Mimes.Add("aiff", "audio/x-aiff");
            Mimes.Add("asf", "video/x-ms-asf");
            Mimes.Add("asr", "video/x-ms-asf");
            Mimes.Add("asx", "video/x-ms-asf");
            Mimes.Add("au", "audio/basic");
            Mimes.Add("avi", "video/x-msvideo");
            Mimes.Add("axs", "application/olescript");
            Mimes.Add("bas", "text/plain");
            Mimes.Add("bcpio", "application/x-bcpio");
            Mimes.Add("bin", "application/octet-stream");
            Mimes.Add("bmp", "image/bmp");
            Mimes.Add("c", "text/plain");
            Mimes.Add("cat", "application/vnd.ms-pkiseccat");
            Mimes.Add("cdf", "application/x-cdf");
            Mimes.Add("cer", "application/x-x509-ca-cert");
            Mimes.Add("class", "application/octet-stream");
            Mimes.Add("clp", "application/x-msclip");
            Mimes.Add("cmx", "image/x-cmx");
            Mimes.Add("cod", "image/cis-cod");
            Mimes.Add("cpio", "application/x-cpio");
            Mimes.Add("crd", "application/x-mscardfile");
            Mimes.Add("crl", "application/pkix-crl");
            Mimes.Add("crt", "application/x-x509-ca-cert");
            Mimes.Add("csh", "application/x-csh");
            Mimes.Add("css", "text/css");
            Mimes.Add("dcr", "application/x-director");
            Mimes.Add("der", "application/x-x509-ca-cert");
            Mimes.Add("dir", "application/x-director");
            Mimes.Add("dll", "application/x-msdownload");
            Mimes.Add("dms", "application/octet-stream");
            Mimes.Add("doc", "application/msword");
            Mimes.Add("dot", "application/msword");
            Mimes.Add("dvi", "application/x-dvi");
            Mimes.Add("dxr", "application/x-director");
            Mimes.Add("eps", "application/postscript");
            Mimes.Add("etx", "text/x-setext");
            Mimes.Add("evy", "application/envoy");
            Mimes.Add("exe", "application/octet-stream");
            Mimes.Add("fif", "application/fractals");
            Mimes.Add("flr", "x-world/x-vrml");            
            Mimes.Add("gtar", "application/x-gtar");
            Mimes.Add("gz", "application/x-gzip");
            Mimes.Add("h", "text/plain");
            Mimes.Add("hdf", "application/x-hdf");
            Mimes.Add("hlp", "application/winhlp");
            Mimes.Add("hqx", "application/mac-binhex40");
            Mimes.Add("hta", "application/hta");
            Mimes.Add("htc", "text/x-component");
            Mimes.Add("htm", "text/html");
            Mimes.Add("html", "text/html");
            Mimes.Add("htt", "text/webviewhtml");
            Mimes.Add("ico", "image/x-icon");
            Mimes.Add("ief", "image/ief");
            Mimes.Add("iii", "application/x-iphone");
            Mimes.Add("ins", "application/x-internet-signup");
            Mimes.Add("isp", "application/x-internet-signup");
            Mimes.Add("jfif", "image/pipeg");
            Mimes.Add("png", "image/png");
            Mimes.Add("gif", "image/gif");
            Mimes.Add("jpg", "image/jpeg");
            Mimes.Add("jpe", "image/jpeg");
            Mimes.Add("jpeg", "image/jpeg");            
            Mimes.Add("js", "application/x-javascript");
            Mimes.Add("latex", "application/x-latex");
            Mimes.Add("lha", "application/octet-stream");
            Mimes.Add("lsf", "video/x-la-asf");
            Mimes.Add("lsx", "video/x-la-asf");
            Mimes.Add("lzh", "application/octet-stream");
            Mimes.Add("m13", "application/x-msmediaview");
            Mimes.Add("m14", "application/x-msmediaview");
            Mimes.Add("m3u", "audio/x-mpegurl");
            Mimes.Add("man", "application/x-troff-man");
            Mimes.Add("mdb", "application/x-msaccess");
            Mimes.Add("me", "application/x-troff-me");
            Mimes.Add("mht", "message/rfc822");
            Mimes.Add("mhtml", "message/rfc822");
            Mimes.Add("mid", "audio/mid");
            Mimes.Add("mny", "application/x-msmoney");
            Mimes.Add("mov", "video/quicktime");
            Mimes.Add("movie", "video/x-sgi-movie");
            Mimes.Add("mp2", "video/mpeg");
            Mimes.Add("mp3", "audio/mpeg");
            Mimes.Add("mpa", "video/mpeg");
            Mimes.Add("mpe", "video/mpeg");
            Mimes.Add("mpeg", "video/mpeg");
            Mimes.Add("mpg", "video/mpeg");
            Mimes.Add("mpp", "application/vnd.ms-project");
            Mimes.Add("mpv2", "video/mpeg");
            Mimes.Add("ms", "application/x-troff-ms");
            Mimes.Add("mvb", "application/x-msmediaview");
            Mimes.Add("nws", "message/rfc822");
            Mimes.Add("oda", "application/oda");
            Mimes.Add("p10", "application/pkcs10");
            Mimes.Add("p12", "application/x-pkcs12");
            Mimes.Add("p7b", "application/x-pkcs7-certificates");
            Mimes.Add("p7c", "application/x-pkcs7-mime");
            Mimes.Add("p7m", "application/x-pkcs7-mime");
            Mimes.Add("p7r", "application/x-pkcs7-certreqresp");
            Mimes.Add("p7s", "application/x-pkcs7-signature");
            Mimes.Add("pbm", "image/x-portable-bitmap");
            Mimes.Add("pdf", "application/pdf");
            Mimes.Add("pfx", "application/x-pkcs12");
            Mimes.Add("pgm", "image/x-portable-graymap");
            Mimes.Add("pko", "application/ynd.ms-pkipko");
            Mimes.Add("pma", "application/x-perfmon");
            Mimes.Add("pmc", "application/x-perfmon");
            Mimes.Add("pml", "application/x-perfmon");
            Mimes.Add("pmr", "application/x-perfmon");
            Mimes.Add("pmw", "application/x-perfmon");
            Mimes.Add("pnm", "image/x-portable-anymap");
            Mimes.Add("pot,", "application/vnd.ms-powerpoint");
            Mimes.Add("ppm", "image/x-portable-pixmap");
            Mimes.Add("pps", "application/vnd.ms-powerpoint");
            Mimes.Add("ppt", "application/vnd.ms-powerpoint");
            Mimes.Add("prf", "application/pics-rules");
            Mimes.Add("ps", "application/postscript");
            Mimes.Add("pub", "application/x-mspublisher");
            Mimes.Add("qt", "video/quicktime");
            Mimes.Add("ra", "audio/x-pn-realaudio");
            Mimes.Add("ram", "audio/x-pn-realaudio");
            Mimes.Add("ras", "image/x-cmu-raster");
            Mimes.Add("rgb", "image/x-rgb");
            Mimes.Add("rmi", "audio/mid");
            Mimes.Add("roff", "application/x-troff");
            Mimes.Add("rtf", "application/rtf");
            Mimes.Add("rtx", "text/richtext");
            Mimes.Add("scd", "application/x-msschedule");
            Mimes.Add("sct", "text/scriptlet");
            Mimes.Add("setpay", "application/set-payment-initiation");
            Mimes.Add("setreg", "application/set-registration-initiation");
            Mimes.Add("sh", "application/x-sh");
            Mimes.Add("shar", "application/x-shar");
            Mimes.Add("sit", "application/x-stuffit");
            Mimes.Add("snd", "audio/basic");
            Mimes.Add("spc", "application/x-pkcs7-certificates");
            Mimes.Add("spl", "application/futuresplash");
            Mimes.Add("src", "application/x-wais-source");
            Mimes.Add("sst", "application/vnd.ms-pkicertstore");
            Mimes.Add("stl", "application/vnd.ms-pkistl");
            Mimes.Add("stm", "text/html");
            Mimes.Add("svg", "image/svg+xml");
            Mimes.Add("sv4cpio", "application/x-sv4cpio");
            Mimes.Add("sv4crc", "application/x-sv4crc");
            Mimes.Add("swf", "application/x-shockwave-flash");
            Mimes.Add("t", "application/x-troff");
            Mimes.Add("tar", "application/x-tar");
            Mimes.Add("tcl", "application/x-tcl");
            Mimes.Add("tex", "application/x-tex");
            Mimes.Add("texi", "application/x-texinfo");
            Mimes.Add("texinfo", "application/x-texinfo");
            Mimes.Add("tgz", "application/x-compressed");
            Mimes.Add("tif", "image/tiff");
            Mimes.Add("tiff", "image/tiff");
            Mimes.Add("tr", "application/x-troff");
            Mimes.Add("trm", "application/x-msterminal");
            Mimes.Add("tsv", "text/tab-separated-values");
            Mimes.Add("txt", "text/plain");
            Mimes.Add("uls", "text/iuls");
            Mimes.Add("ustar", "application/x-ustar");
            Mimes.Add("vcf", "text/x-vcard");
            Mimes.Add("vrml", "x-world/x-vrml");
            Mimes.Add("wav", "audio/x-wav");
            Mimes.Add("wcm", "application/vnd.ms-works");
            Mimes.Add("wdb", "application/vnd.ms-works");
            Mimes.Add("wks", "application/vnd.ms-works");
            Mimes.Add("wmf", "application/x-msmetafile");
            Mimes.Add("wps", "application/vnd.ms-works");
            Mimes.Add("wri", "application/x-mswrite");
            Mimes.Add("wrl", "x-world/x-vrml");
            Mimes.Add("wrz", "x-world/x-vrml");
            Mimes.Add("xaf", "x-world/x-vrml");
            Mimes.Add("xbm", "image/x-xbitmap");
            Mimes.Add("xla", "application/vnd.ms-excel");
            Mimes.Add("xlc", "application/vnd.ms-excel");
            Mimes.Add("xlm", "application/vnd.ms-excel");
            Mimes.Add("xls", "application/vnd.ms-excel");
            Mimes.Add("xlt", "application/vnd.ms-excel");
            Mimes.Add("xlw", "application/vnd.ms-excel");
            Mimes.Add("xof", "x-world/x-vrml");
            Mimes.Add("xpm", "image/x-xpixmap");
            Mimes.Add("xwd", "image/x-xwindowdump");
            Mimes.Add("z", "application/x-compress");
            Mimes.Add("zip", "application/zip");
            #endregion
            MimesReverse = new Dictionary<string, string>(Mimes.Count);
            foreach(KeyValuePair<string,string> pair in Mimes)
            {
                if (!MimesReverse.ContainsKey(pair.Value))
                {
                    MimesReverse.Add(pair.Value, pair.Key);
                }                
            }
        }

        /// <summary>
        /// 根据文件后缀名获取对应的mime类型
        /// </summary>
        /// <param name="ext">后缀名（带点不带点都行）</param>
        /// <returns>返回文件对应的mime类型，如果没有找到，则返回默认的mime类型</returns>
        public static string GetMimeType(string ext)
        {
            ext=ext.Replace(".","").ToLower();
            if (Mimes.ContainsKey(ext))
            {
                return Mimes[ext];
            }
            return DefaultMime;
        }

        /// <summary>
        /// 根据mime类型获取对应的文件后缀名
        /// </summary>
        /// <param name="mimeType">mime而徐</param>
        /// <returns>返回mime类型对应的文件后缀名，如果没有找到，则返回空字符串</returns>
        public static string GetExtension(string mimeType)
        {
            if (null!=mimeType)
            {
                mimeType = mimeType.ToLower().Trim();
                if (MimesReverse.ContainsKey(mimeType))
                {
                    return MimesReverse[mimeType];
                }
            }           
            return "";
        }

    }
}
