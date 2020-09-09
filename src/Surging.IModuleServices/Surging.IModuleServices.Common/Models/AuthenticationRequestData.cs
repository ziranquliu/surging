using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.IModuleServices.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    [ProtoContract]
    public class AuthenticationRequestData
    {
        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(1)]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(2)]
        public string Password { get; set; }
    }
}
