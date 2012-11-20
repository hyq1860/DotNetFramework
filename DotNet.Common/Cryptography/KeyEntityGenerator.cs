// -----------------------------------------------------------------------
// <copyright file="KeyEntityGenerator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography;

namespace DotNet.Common.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 產生AES需要的Key與IV
    /// http://www.dotblogs.com.tw/hatelove/archive/2011/11/15/.net-generator-aes-key-iv.aspx
    /// </summary>
    public static class KeyEntityGenerator
    {
        /// <summary>
        /// 透過RijndaelManaged產生AES的Key與IV，並經過Base64轉換成字串
        /// </summary>
        /// <returns>帶有Key與IV的KeyEntity</returns>
        public static void GetKeyEntity()
        {
            var generator = new RijndaelManaged();
            var key = Convert.ToBase64String(generator.Key);
            var iv = Convert.ToBase64String(generator.IV);
        }
    }

}
