using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class EventAggregateRoot : IAggregateRoot
    {

        public EventAggregateRoot(Event @event)
        {
            if(@event == null)
            {
                throw new InvalidOperationException("Event cannot be null ofr the root");
            }
            this.Event = @event;
        }

        public Event Event
        {
            get;
            private set;
        }
    }
}
