// -----------------------------------------------------------------------
// <copyright file="EncodingTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNetTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EncodingTest
    {
        public static void Test()
        {
            IdentifyEncoding sinodetector;
            string result = null;
            sinodetector = new IdentifyEncoding();

            try
            {
                result = sinodetector.GetEncodingName(new System.Uri("http://china5.nikkeibp.co.jp/china/news/com/200307/pr_com200307170131.html"));
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine("Bad URL " + e.ToString());
            }
            Console.Write(result);
            Console.Read();
        }

    }
}
