// -----------------------------------------------------------------------
// <copyright file="ProcessWather.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using DotNet.Common.Configuration;
using DotNet.Common.Core;
using DotNet.Common.Logging;

namespace DotNet.SpiderApplication.Client
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 进程监视启动器
    /// </summary>
    public class ProcessWatcher
    {
        /// <summary>
        /// 进程配置使用绝对路径还是相对路径
        /// true 使用绝对路径 false 使用相对路径（即加上Environment.CurrentDirectory）
        /// </summary>
        public bool UseAbsolutePath { get; set; }

        private ProcessWatcher()
        {
            try
            {
                // 读取监控进程全路径
                string strProcessAddress = ConfigHelper.ParamsConfig.GetParamValue("demo1");
                if (this.processAddress == null)
                {
                    this.processAddress = new List<string>();
                }

                if (strProcessAddress.Trim() != string.Empty)
                {
                    if (this.UseAbsolutePath)
                    {
                        this.processAddress = strProcessAddress.Split(',').ToList();
                    }
                    else
                    {
                        var processArray = strProcessAddress.Split(',');
                        foreach (var s in processArray)
                        {
                            this.processAddress.Add(Environment.CurrentDirectory + s);
                        }
                    }
                }
                else
                {
                    throw new Exception("读取配置档ProcessAddress失败，ProcessAddress为空！");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Watcher()初始化出错！错误描述为：" + ex.Message);
            }
        }

        private List<string> processAddress;

        /// <summary>
        /// 开始监控
        /// </summary>
        public void StartWatch()
        {
            if (this.processAddress != null)
            {
                if (this.processAddress.Count > 0)
                {
                    foreach (string str in processAddress)
                    {
                        if (str.Trim() != string.Empty)
                        {
                            if (File.Exists(str.Trim()))
                            {
                                this.ScanProcessList(str.Trim());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 扫描进程列表，判断进程对应的全路径是否与指定路径一致
        /// 如果一致，说明进程已启动
        /// 如果不一致，说明进程尚未启动
        /// </summary>
        private void ScanProcessList(string address)
        {
            Process[] arrayProcess = Process.GetProcesses();
            foreach (Process p in arrayProcess)
            {
                // System、Idle进程会拒绝访问其全路径
                if (p.ProcessName != "System" && p.ProcessName != "Idle")
                {
                    try
                    {
                        if (this.FormatPath(address) == this.FormatPath(p.MainModule.FileName))
                        {
                            // 进程已启动
                            this.WatchProcess(p, address);
                            return;
                        }
                    }
                    catch
                    {
                        // 拒绝访问进程的全路径
                        Logger.Log("进程(" + p.Id.ToString() + ")(" + p.ProcessName + ")拒绝访问全路径！");
                    }
                }
            }

            // 进程尚未启动
            Process process = new Process();
            process.StartInfo.FileName = address;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.ErrorDialog = true;
            process.Start();
            this.WatchProcess(process, address);
        }

        /// <summary>
        /// 格式化路径
        /// 去除前后空格
        /// 去除最后的"\"
        /// 字母全部转化为小写
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string FormatPath(string path)
        {
            return path.ToLower().Trim().TrimEnd('\\');
        }

        /// <summary>
        /// 监听进程
        /// </summary>
        /// <param name="p"></param>
        /// <param name="address"></param>
        private void WatchProcess(Process process, string address)
        {
            ProcessRestart objProcessRestart = new ProcessRestart(process, address);
            Thread thread = new Thread(new ThreadStart(objProcessRestart.RestartProcess));
            thread.Start();
        }
    }

    public class ProcessRestart
    {
        //字段
        private Process _process;
        private string _address;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcessRestart()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="process"></param>
        /// <param name="address"></param>
        public ProcessRestart(Process process, string address)
        {
            this._process = process;
            this._address = address;
        }

        /// <summary>
        /// 重启进程
        /// </summary>
        public void RestartProcess()
        {
            try
            {
                while (true)
                {
                    this._process.WaitForExit();
                    this._process.Close();    //释放已退出进程的句柄
                    //Thread.Sleep(5000);
                    this._process.StartInfo.FileName = this._address;
                    _process.StartInfo.CreateNoWindow = true;
                    _process.StartInfo.UseShellExecute = false;
                    _process.StartInfo.ErrorDialog = true;
                    this._process.Start();

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                //ProcessWatcher objProcessWatcher = SingletonProvider<ProcessWatcher>.UniqueInstance;
                Logger.Log("RestartProcess() 出错，监控程序已取消对进程("
                    + this._process.Id.ToString() + ")(" + this._process.ProcessName
                    + ")的监控，错误描述为：" + ex.Message);
            }
        }
    }
}
