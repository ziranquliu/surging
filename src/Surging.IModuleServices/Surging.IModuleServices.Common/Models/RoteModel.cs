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
    public class RoteModel
    {
        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(1)]
        public string ServiceId { get; set; }
    }
}
