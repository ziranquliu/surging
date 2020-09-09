using Greet;
using Grpc.Core;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System.Threading.Tasks;

namespace Surging.IModuleServices.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBundle("api/{Service}/{Method}")]
    public interface IGreeterService : IServiceKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context = null);
    }
}
