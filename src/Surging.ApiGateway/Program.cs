using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Surging.Core.ApiGateWay;
using Surging.Core.Codec.MessagePack;
using Surging.Core.Consul;
using Surging.Core.Consul.Configurations;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.DotNetty;
using Surging.Core.ProxyGenerator;
using Surging.Core.ServiceHosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Surging.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //RunCmd("../StartConsul.bat");

            var host = new WebHostBuilder()
             .UseUrls("http://*:729")
             .UseKestrel()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseIISIntegration()
             .UseStartup<Startup>()
             .Build();
            host.Run();
        }

        /// <summary>
        /// 执行CMD语句
        /// </summary>
        /// <param name="cmd">要执行的CMD命令</param>
        public static void RunCmd(string cmd)
        {
            foreach (Process process in Process.GetProcessesByName("consul"))
            {
                //Console.WriteLine(process.ProcessName);
                process.Kill();
            }

            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine(cmd);
            proc.StandardInput.WriteLine("exit");
            //string outStr = proc.StandardOutput.ReadToEnd();
            proc.Close();
            //return outStr;
        }
    }
}

