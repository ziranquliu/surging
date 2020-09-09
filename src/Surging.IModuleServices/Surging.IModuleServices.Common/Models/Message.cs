using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.IModuleServices.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
   public class Message
    {
        /// <summary>
        /// 
        /// </summary>
        public string RoutePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object> Parameters { get; set; }
    }
}
