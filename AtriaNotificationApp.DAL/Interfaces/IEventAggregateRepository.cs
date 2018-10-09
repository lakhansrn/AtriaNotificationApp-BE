using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtriaNotificationApp.DAL.Entities;

namespace AtriaNotificationApp.DAL.Interfaces
{
    public interface IEventAggregateRepository
    {
        Task<IEnumerable<EventAggregateRoot>> GetAllEventRoots();

        Task<Event> AddEvent(Event @event);

        Task<IEnumerable<Event>> AddEvents(IEnumerable<Event> events);

        Task<EventAggregateRoot> GetEventsByAnnouncmentID(Guid guid);

    }
}
