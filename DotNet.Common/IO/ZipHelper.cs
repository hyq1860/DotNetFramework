// -----------------------------------------------------------------------
// <copyright file="ZipHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace DotNet.Common.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// zip文件压缩组手
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 存放待压缩的文件的绝对路径
        /// </summary>
        private List<string> AbsolutePaths { set; get; }
        public string errorMsg { set; get; }

        public ZipHelper()
        {
            errorMsg = "";
            AbsolutePaths = new List<string>();
        }
        /// <summary>
        /// 添加压缩文件
        /// </summary>
        /// <param name="_fileAbsolutePath">文件的绝对路径</param>
        public void AddFile(string _fileAbsolutePath)
        {
            AbsolutePaths.Add(_fileAbsolutePath);
        }
        /// <summary>
        /// 压缩文件或者文件夹
        /// </summary>
        /// <param name="_depositPath">压缩后文件的存放路径   如C:\\windows\abc.zip</param>
        /// <returns></returns>
        public bool CompressionZip(string _depositPath)
        {
            bool result = true;
            FileStream fs = null;
            try
            {
                ZipOutputStream ComStream = new ZipOutputStream(File.Create(_depositPath));
                ComStream.SetLevel(9);      //压缩等级
                foreach (string path in AbsolutePaths)
                {
                    //如果是目录
                    if (Directory.Exists(path))
                    {
                        ZipFloder(path, ComStream, path);
                    }
                    else if (File.Exists(path))//如果是文件
                    {
                         fs = File.OpenRead(path);
                        byte[] bts = new byte[fs.Length];
                        fs.Read(bts, 0, bts.Length);
                        ZipEntry ze = new ZipEntry(new FileInfo(path).Name);
                        ComStream.PutNextEntry(ze);             //为压缩文件流提供一个容器
                        ComStream.Write(bts, 0, bts.Length);  //写入字节
                    }
                }
                ComStream.Finish(); // 结束压缩
                ComStream.Close();
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                errorMsg = ex.Message;
                result = false;
            }
            return result;
        }
        //压缩文件夹
        private void ZipFloder(string _OfloderPath, ZipOutputStream zos, string _floderPath)
        {
            foreach (FileSystemInfo item in new DirectoryInfo(_floderPath).GetFileSystemInfos())
            {
                if (Directory.Exists(item.FullName))
                {
                    ZipFloder(_OfloderPath, zos, item.FullName);
                }
                else if (File.Exists(item.FullName))//如果是文件
                {
                    DirectoryInfo ODir = new DirectoryInfo(_OfloderPath);
                    string fullName2 = new FileInfo(item.FullName).FullName;
                    string path = ODir.Name + fullName2.Substring(ODir.FullName.Length, fullName2.Length - ODir.FullName.Length);//获取相对目录
                    FileStream fs = File.OpenRead(fullName2);
                    byte[] bts = new byte[fs.Length];
                    fs.Read(bts, 0, bts.Length);
                    ZipEntry ze = new ZipEntry(path);
                    zos.PutNextEntry(ze);             //为压缩文件流提供一个容器
                    zos.Write(bts, 0, bts.Length);  //写入字节
                }
            }
        }
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="_depositPath">压缩文件路径</param>
        /// <param name="_floderPath">解压的路径</param>
        /// <returns></returns>
        public bool DeCompressionZip(string _depositPath, string _floderPath)
        {
            bool result = true;
            FileStream fs=null;
            try
            {
                ZipInputStream InpStream = new ZipInputStream(File.OpenRead(_depositPath));
                ZipEntry ze = InpStream.GetNextEntry();//获取压缩文件中的每一个文件
                Directory.CreateDirectory(_floderPath);//创建解压文件夹
                while (ze != null)//如果解压完ze则是null
                {
                    if (ze.IsFile)//压缩zipINputStream里面存的都是文件。带文件夹的文件名字是文件夹\\文件名
                    {
                        string[] strs=ze.Name.Split('\\');//如果文件名中包含’\\‘则表明有文件夹
                        if (strs.Length > 1)
                        {
                            //两层循环用于一层一层创建文件夹
                            for (int i = 0; i < strs.Length-1; i++)
                            {
                                string floderPath=_floderPath;
                                for (int j = 0; j < i; j++)
                                {
                                    floderPath = floderPath + "\\" + strs[j];
                                }
                                floderPath=floderPath+"\\"+strs[i];
                                Directory.CreateDirectory(floderPath);
                            }
                        }
                         fs = new FileStream(_floderPath+"\\"+ze.Name, FileMode.OpenOrCreate, FileAccess.Write);//创建文件
                        //循环读取文件到文件流中
                        while (true)
                        {
                            byte[] bts = new byte[1024];
                           int i= InpStream.Read(bts, 0, bts.Length);
                           if (i > 0)
                           {
                               fs.Write(bts, 0, i);
                           }
                           else
                           {
                               fs.Flush();
                               fs.Close();
                               break;
                           }
                        }
                    }
                    ze = InpStream.GetNextEntry();
                }
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                errorMsg = ex.Message;
                result = false;
            }
            return result;
        }

    }
}
