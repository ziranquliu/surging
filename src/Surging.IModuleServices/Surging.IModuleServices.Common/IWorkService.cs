using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.IModuleServices.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surging.IModuleServices.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBundle("Background/{Service}")]
    public interface IWorkService : IServiceKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> AddWork(Message message);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         Task StartAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
    }
}
