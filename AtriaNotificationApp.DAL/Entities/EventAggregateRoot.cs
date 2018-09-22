using AtriaNotificationApp.Common.Interfaces;
using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class EventAggregateRoot : IEventAggregateRoot
    {

        public EventAggregateRoot(Event @event)
        {
            this.Event = @event;
        }

        public Event Event
        {
            get;
            private set;
        }
    }
}
