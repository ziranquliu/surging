using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.IModuleServices.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WillMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public bool WillRetain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Qos { get; set; }
    }
}
