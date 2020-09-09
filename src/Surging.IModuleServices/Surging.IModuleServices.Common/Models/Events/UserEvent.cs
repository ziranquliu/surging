using Surging.Core.CPlatform.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.IModuleServices.Common.Models.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEvent : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }
    }
}
