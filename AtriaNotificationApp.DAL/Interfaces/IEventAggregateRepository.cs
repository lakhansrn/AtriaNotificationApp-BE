using AtriaNotificationApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IEventAggregateRepository
    {
        IEnumerable<EventAggregateRoot> GetAllEventRoots();

    }
}
