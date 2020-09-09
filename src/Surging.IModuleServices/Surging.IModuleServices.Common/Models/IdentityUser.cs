using ProtoBuf;
using Surging.Core.CPlatform;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.IModuleServices.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    [ProtoContract]
    public class IdentityUser:RequestData
    {
        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(1)]
        public string RoleId { get; set; }
    }
}
