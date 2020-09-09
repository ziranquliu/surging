using Surging.Core.CPlatform.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.IModuleServices.Common.Models.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class LogoutEvent : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Age { get; set; }
    }
}
