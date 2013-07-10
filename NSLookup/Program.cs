using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSLookup
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Net;

    internal class NSLookup
    {
        private static int Main()
        {
            var addressArray = File.ReadAllLines("c:\\www.txt");
            NsLookup(addressArray.ToList());
            return 0;
        }

        public static void NsLookup(List<string> addresses)
        {
            if (!addresses.Any())
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var address in addresses)
            {
                //We have something, try to look it up....

                RunCmd("nslookup " + address, 0);
                //    //The IP or Host Entry to lookup
                //    IPHostEntry ipEntry;
                //    //The IP Address Array. Holds an array of resolved Host Names.
                //    IPAddress[] ipAddr;
                //    //Value of alpha characters
                //    char[] alpha = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ-".ToCharArray();
                //    //If alpha characters exist we know we are doing a forward lookup
                //    if (address.IndexOfAny(alpha) != -1)
                //    {
                //        ipEntry = Dns.GetHostEntry(address);
                //        if (ipEntry != null)
                //        {
                //            bool cdn = ipEntry.HostName.Contains("");
                //            if(ipEntry.Aliases.Length > 1)
                //            {
                //                cdn = true;
                //            }
                //            sb.AppendLine(
                //                string.Format("{0}:{1}",  cdn? "有cdn" : "无cdn"));
                //        }
                //    }
                //    //If no alpha characters exist we do a reverse lookup
                //    else
                //    {
                //        ipEntry = Dns.Resolve(address);
                //        if (ipEntry != null && ipEntry.Aliases != null)
                //        {
                //            sb.AppendLine(
                //                string.Format("{0}:{1}", address, ipEntry.Aliases.Length > 1 ? "有cdn" : "无cdn"));
                //        }
                //    }
                //}
                //catch (System.Net.Sockets.SocketException se)
                //{
                //    sb.AppendLine(string.Format("{0}:{1}", address, se.Message.ToString(CultureInfo.InvariantCulture)));
                //}
                //catch (System.FormatException fe)
                //{
                //    sb.AppendLine(string.Format("{0}:{1}", address, fe.Message.ToString(CultureInfo.InvariantCulture)));
                //}


                File.WriteAllText("c:\\扫描结果.txt", sb.ToString());
            }
        }

        private static string RunCmd(string command, int milliseconds)
        {            
            
            Process p = new Process();
            string res = string.Empty;

            p.StartInfo.FileName = "cmd.exe";          
            p.StartInfo.Arguments = command;    
            p.StartInfo.UseShellExecute = false;        
            p.StartInfo.RedirectStandardInput = true;   
            p.StartInfo.RedirectStandardOutput = true;  
            p.StartInfo.RedirectStandardError = true;   
            p.StartInfo.CreateNoWindow = true;          

            try
            {
                if (p.Start())       //开始进程
                {
                    if (milliseconds == 0)
                        p.WaitForExit();     //这里无限等待进程结束
                    else
                        p.WaitForExit(milliseconds);  //这里等待进程结束，等待时间为指定的毫秒     

                    res = p.StandardOutput.ReadToEnd();
                }
            }
            catch
            {
            }
            finally
            {
                if (p != null)
                    p.Close();
            }
                        
            return res;       
        }
    }
}
