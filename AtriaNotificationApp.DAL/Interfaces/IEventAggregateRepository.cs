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

        Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Content content);

        Task<Event> UpdateContent(Guid event_guid, Guid announcement_guid, Guid content_id, Content content);
    }
}
