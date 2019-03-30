using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtriaNotificationApp.DAL.Entities;

namespace AtriaNotificationApp.BL.Interfaces
{
    public interface IEventProviderService
    {
        Task<IEnumerable<Event>> GetAllValidEvents();

        Task<Event> AddEvent(Event @event);
        Task<Event> UpdateEvent(Event @event);
        Task<IEnumerable<Event>> AddEvents(IEnumerable<Event> events);
        Task<Event> AddAnnouncement(Guid eventid, Announcement announcement);
        Task<Announcement> UpdateAnnouncement(Guid eventid, Announcement announcement);
        Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Content content);
        Task<Event> UpdateContent(Guid event_guid, Guid announcement_guid, Guid content_id, Content content);
        Task DeleteEvent(Guid guid);
    }
}
