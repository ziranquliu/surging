using ProtoBuf;
using Surging.Core.System.Intercept;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.IModuleServices.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    [ProtoContract]
    public class BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(1)]
        public Guid Id => Guid.NewGuid();
    }
}
