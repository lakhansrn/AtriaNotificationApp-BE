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
        Task<IEnumerable<Event>> AddEvents(IEnumerable<Event> events);
        Task<Event> AddContent(Guid event_guid, Guid announcement_guid, Content content);
        Task<Event> UpdateContent(Guid event_guid, Guid announcement_guid, Guid content_id, Content content);

		Task<Event> AddAnnouncement(Guid guid, Announcement announcement);

		Task<Event> UpdateAnnouncement(Guid guid, Guid announcment_guid, Announcement announcement);
    }
}
