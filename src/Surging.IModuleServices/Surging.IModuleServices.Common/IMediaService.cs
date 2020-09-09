using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.Protocol.WS.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surging.IModuleServices.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBundle("Api/{Service}")]
    [BehaviorContract(IgnoreExtensions =true,Protocol = "media")]
    public interface IMediaService : IServiceKey
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task Push(IEnumerable<byte> data);
    }

}
