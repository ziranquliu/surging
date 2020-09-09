using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.CPlatform.Support.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Surging.IModuleServices.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBundle("api/{Service}/{Method}")]
    public interface IAsyncService: ThriftCore.Calculator.IAsync,  IServiceKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       [Command(ExecutionTimeoutInMilliseconds=10000)]
        Task<int> @AddAsync(int num1, int num2, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> SayHelloAsync(CancellationToken cancellationToken = default);
    }
}
